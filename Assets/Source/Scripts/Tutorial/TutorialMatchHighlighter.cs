using Assets.Source.Scripts.ActionsHandlers;
using Assets.Source.Scripts.Extensions;
using Assets.Source.Scripts.Factory;
using Assets.Source.Scripts.GameBehaviour;
using Assets.Source.Scripts.InteractiveObjects;
using Assets.Source.Scripts.MagicCells;
using Assets.Source.Scripts.Pool;
using Assets.Source.Scripts.UI.Buttons;
using Assets.Source.Scripts.Vessels;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.Tutorial
{
    public class TutorialMatchHighlighter : MonoBehaviour
    {
        [SerializeField] private GameSessionHandler _gameHandler;
        [SerializeField] private AnimationParticle _animationParticle;
        [SerializeField] private MagicCellRouter _cellRouter;
        [SerializeField] private MagicColumnPool _magicColumnPool;
        [SerializeField] private VesselPool _vesselPool;
        [SerializeField] private VesselFactory _vesselFactory;
        [SerializeField] private WaitingPoint _waitingPoint;
        [SerializeField] private ClickModeSwitcher _modeSwitcher;
        [SerializeField] private DelayedActionTimer _timer;
        [SerializeField] private Transform[] _columns;

        private float _beginsearchInterval = 1.5f;
        private float _searchInterval = 8f;
        private ButtonRewardedAdv _rewardedButton;
        private ReverseButton _reverseButton;
        private List<MagicColumn> _currentColumns;
        private List<Vessel> _currentVessels;

        private void Awake()
        {
            _currentVessels = new List<Vessel>();
            _currentColumns = new List<MagicColumn>();

            ValidateDependencies();
        }

        private void Start()
        {
            _timer.StartTimer(_beginsearchInterval, PerformAnalysis);

        }

        private void OnEnable()
        {
            _gameHandler.GameReseting += OnStartInitialSearch;
            _cellRouter.CellDeparturing += OnStartSearchLoop;
        }

        private void OnDisable()
        {
            _gameHandler.GameReseting -= OnStartInitialSearch;
            _cellRouter.CellDeparturing -= OnStartSearchLoop;

            if (_reverseButton != null)
                _modeSwitcher.ReverseButtonActivating -= OnReverseButtonActivated;

            _timer.StopTimer();
        }

        public void SetButtonRewarded(
            ButtonRewardedAdv rewardedButton,
            ReverseButton reverseButton)
        {
            Guard.NotNull(rewardedButton, nameof(rewardedButton));
            Guard.NotNull(reverseButton, nameof(reverseButton));

            _rewardedButton = rewardedButton;
            _reverseButton = reverseButton;

            _modeSwitcher.ReverseButtonActivating += OnReverseButtonActivated;
        }

        private void OnStartSearchLoop()
        {
            _animationParticle.Stop();
            _timer.StartTimer(_searchInterval, PerformAnalysis);
        }

        private void OnStartInitialSearch()
        {
            _animationParticle.Stop();
            _timer.StartTimer(_beginsearchInterval, PerformAnalysis);
        }

        private void OnReverseButtonActivated()
        {
            LoadCurrentColumns();
            LoadCurrentVessels();

            MagicColumn targetColumn = FindReverseColumn();
            StackMagicCells stack = targetColumn.GetComponent<StackMagicCells>();
            MagicCell cell = stack.GetUpperCell();

            if (cell != null)
                _animationParticle.Play(cell.transform.position);
        }

        private MagicColumn FindReverseColumn()
        {
            foreach (Vessel vessel in _currentVessels)
            {
                Color color = vessel.Color;

                foreach (MagicColumn column in _currentColumns)
                {
                    StackMagicCells stack = column.GetComponent<StackMagicCells>();
                    MagicCell bottomCell = stack.GetBottomCell();

                    if (bottomCell != null && bottomCell.Color == color)
                        return column;
                }
            }

            return null;
        }

        public void LoadCurrentColumns()
        {
            _currentColumns.Clear();
            _currentColumns.AddRange(
                _magicColumnPool.GetActiveObjects());
        }

        public void LoadCurrentVessels()
        {
            _currentVessels.Clear();

            foreach (var vessel in _vesselFactory.Objects)
            {
                if (vessel.IsActive)
                    _currentVessels.Add(vessel);
            }
        }

        private void PerformAnalysis()
        {
            LoadCurrentColumns();
            LoadCurrentVessels();
            FindMatch();
        }

        private void FindMatch()
        {
            MagicCell firstWrongCell = null;
            bool hasMatchInColumns = false;

            foreach (Vessel vessel in _currentVessels)
            {
                Color color = vessel.Color;

                foreach (MagicColumn column in _currentColumns)
                {
                    StackMagicCells stack = column.GetComponent<StackMagicCells>();
                    MagicCell topCell = stack.TryGetCellByColor(color);

                    if (topCell != null)
                    {
                        hasMatchInColumns = true;

                        PlayAnimationCell(topCell);

                        return;
                    }

                    if (firstWrongCell == null)
                    {
                        MagicCell upperCell = stack.GetBottomCell();

                        if (upperCell != null)
                            firstWrongCell = upperCell;
                    }
                }
            }

            ResolveNoMatchCases(firstWrongCell, hasMatchInColumns);
        }

        private void ResolveNoMatchCases(MagicCell firstWrongCell, bool hasMatchInColumns)
        {
            Button reverseButton = GetComponentButton(_reverseButton);
            Button rewardedButton = GetComponentButton(_rewardedButton);

            if (!hasMatchInColumns && _waitingPoint.IsFreePlace)
            {
                if (firstWrongCell != null)
                    _animationParticle.Play(firstWrongCell.transform.position);

                return;
            }

            if (hasMatchInColumns == false 
                && _waitingPoint.IsFreePlace == false 
                && IsInteractable(_reverseButton))
            {
                _animationParticle.Play(reverseButton);

                return;
            }

            if (hasMatchInColumns == false 
                && _waitingPoint.IsFreePlace == false 
                && IsInteractable(_reverseButton) == false)
            {
                _animationParticle.Play(rewardedButton);
            }
        }

        private void PlayAnimationCell(MagicCell magicCell)
        {
            Guard.NotNull(magicCell, nameof(magicCell));
            _animationParticle.Play(magicCell.transform.position);
        }

        private bool IsInteractable(Component button)
        {
            Button checkingButton = button?.GetComponent<Button>();

            return checkingButton != null && checkingButton.interactable;
        }

        private Button GetComponentButton(Component component)
        {
            if (component == null)
                return null;

            Button button = component.GetComponent<Button>();
            Guard.NotNull(button, nameof(button));

            return button;
        }

        //private void OnStartSearchLoop()
        //{
        //    SearchWithDelay(_searchInterval);
        //}

        //private void OnStartInitialSearch()
        //{
        //    SearchWithDelay(_beginsearchInterval);
        //}

        //private void OnReverseButtonActivated()
        //{
        //    LoadCurrentColumns();
        //    LoadCurrentVessels();

        //    MagicColumn targetColumn = FindReverseColumn();
        //    StackMagicCells stack = targetColumn.GetComponent<StackMagicCells>();
        //    MagicCell cell = stack.GetUpperCell();

        //    if (cell != null)
        //        _animationParticle.Play(cell.transform.position);
        //}

        //private MagicColumn FindReverseColumn()
        //{
        //    foreach (Vessel vessel in _currentVessels)
        //    {
        //        Color color = vessel.Color;

        //        foreach (MagicColumn column in _currentColumns)
        //        {
        //            StackMagicCells stack = column.GetComponent<StackMagicCells>();

        //            MagicCell bottomCell = stack.GetBottomCell();

        //            if (bottomCell != null)
        //            {
        //                if (bottomCell.Color == color)
        //                {
        //                    return column;
        //                }
        //            }
        //        }
        //    }

        //    return null;
        //}

        //private IEnumerator SearchRoutine(float delay)
        //{
        //    yield return new WaitForSeconds(delay);

        //    PerformAnalysis();

        //    _searchRoutine = null;
        //}

        //private void SearchWithDelay(float delay)
        //{
        //    if (_searchRoutine != null)
        //        StopCoroutine(_searchRoutine);

        //    _searchRoutine = StartCoroutine(SearchRoutine(delay));
        //}

        //private void StopSearchLoop()
        //{
        //    if (_searchRoutine != null)
        //    {
        //        StopCoroutine(_searchRoutine);

        //        _searchRoutine = null;
        //    }
        //}

        //private void PerformAnalysis()
        //{
        //    LoadCurrentColumns();
        //    LoadCurrentVessels();
        //    FindMatch();
        //}

        //public void LoadCurrentColumns()
        //{
        //    _currentColumns.Clear();
        //    _currentColumns.AddRange(_magicColumnPool.
        //        GetActiveObjects());
        //}

        //public void LoadCurrentVessels()
        //{
        //    _currentVessels.Clear();

        //    foreach (var vessel in _vesselFactory.Objects)
        //    {
        //        if (vessel.IsActive)
        //            _currentVessels.Add(vessel);
        //    }
        //}

        //private void FindMatch()
        //{
        //    MagicCell firstWrongCell = null;

        //    bool hasMatchInColumns = false;

        //    foreach (Vessel vessel in _currentVessels)
        //    {
        //        Color color = vessel.Color;

        //        foreach (MagicColumn column in _currentColumns)
        //        {
        //            StackMagicCells stack = column.GetComponent<StackMagicCells>();

        //            MagicCell topCell = stack.TryGetCellByColor(color);

        //            if (topCell != null)
        //            {
        //                hasMatchInColumns = true;

        //                PlayAnimationCell(topCell);

        //                return;
        //            }

        //            if (firstWrongCell == null)
        //            {
        //                MagicCell upperCell = stack.GetBottomCell();

        //                if (upperCell != null)
        //                    firstWrongCell = upperCell;
        //            }
        //        }
        //    }

        //    ResolveNoMatchCases(firstWrongCell, hasMatchInColumns);
        //}

        //private void ResolveNoMatchCases(MagicCell firstWrongCell,bool hasMatchInColumns)
        //{
        //    Button reverseButton = GetComponentButton(_reverseButton);
        //    Button rewardedButton = GetComponentButton(_rewardedButton);

        //    if (hasMatchInColumns == false && _waitingPoint.IsFreePlace)
        //    {
        //        if (firstWrongCell != null)
        //            _animationParticle.Play(firstWrongCell.transform.position);

        //        return;
        //    }

        //    if (hasMatchInColumns == false && _waitingPoint.IsFreePlace == false && IsInteractable(_reverseButton))
        //    {
        //        _animationParticle.Play(reverseButton);
        //        return;
        //    }

        //    if (hasMatchInColumns == false && _waitingPoint.IsFreePlace == false && !IsInteractable(_reverseButton))
        //    {
        //        _animationParticle.Play(rewardedButton);
        //        return;
        //    }
        //}

        //private void PlayAnimationCell(MagicCell magicCell)
        //{
        //    if(magicCell == null)
        //        throw new ArgumentNullException(nameof(magicCell));

        //    _animationParticle.Play(magicCell.transform.position);

        //    return;
        //}

        //private bool IsInteractable(Component button)
        //{
        //    Button checkingButton = button.GetComponent<Button>();

        //    return checkingButton != null && checkingButton.interactable;
        //}

        //private Button GetComponentButton(Component component)
        //{
        //    if (component == null)
        //        return null;

        //    Button button = component.GetComponent<Button>();

        //    if (button == null)
        //        throw new ArgumentNullException(nameof(button));

        //    return button;
        //}

        private void ValidateDependencies()
        {
            Guard.NotNull(_animationParticle, nameof(_animationParticle));
            Guard.NotNull(_cellRouter, nameof(_cellRouter));
            Guard.NotNull(_magicColumnPool, nameof(_magicColumnPool));
            Guard.NotNull(_vesselPool, nameof(_vesselPool));
            Guard.NotNull(_waitingPoint, nameof(_waitingPoint));
            Guard.NotNull(_timer, nameof(_timer));

            //if (_animationParticle == null)
            //{
            //    throw new InvalidOperationException(
            //        $"{nameof(_animationParticle)} не назначен в инспекторе.");
            //}

            //if (_cellRouter == null)
            //{
            //    throw new InvalidOperationException(
            //        $"{nameof(_cellRouter)} не назначен в инспекторе.");
            //}

            //if (_magicColumnPool == null)
            //{
            //    throw new InvalidOperationException(
            //        $"{nameof(_magicColumnPool)} не назначен в инспекторе.");
            //}

            //if (_vesselPool == null)
            //{
            //    throw new InvalidOperationException(
            //        $"{nameof(_vesselPool)} не назначен в инспекторе.");
            //}

            //if (_waitingPoint == null)
            //{
            //    throw new InvalidOperationException(
            //        $"{nameof(_waitingPoint)} не назначен или пуст.");
            //}
        }
    }
}
