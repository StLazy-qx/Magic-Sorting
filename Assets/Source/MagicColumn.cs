using System;
using System.Collections.Generic;
using UnityEngine;

public class MagicColumn : MonoBehaviour
{
    private int _countCells;
    private float _prefabHeight;
    private float _distanceBetweenCells = 0.05f;
    private MagicCell _cellPrefab;
    private DistributerMagicCell _distributerMagic;
    private Stack<MagicCell> _cellsStack;
    private BuildMagicColumn _colorSource;

    public event Action CellDisplacing;

    private void Awake()
    {
        _cellsStack = new();
    }

    //void Start()
    //{
    //    CreateCells();
    //}

    public void Initialize(
        MagicCell magicCell, 
        DistributerMagicCell distributerMagicCell, 
        BuildMagicColumn colorSource, 
        int countCells)
    {
        if (countCells <= 0)
            return;

        if (colorSource == null)
        {
            Debug.LogError($"[MagicColumn] colorSource == null у {name}. Столб останется пустым.");
            return;
        }

        _cellPrefab = magicCell;
        _distributerMagic = distributerMagicCell;
        _colorSource = colorSource;
        //может не стоит оставлять переменную глобальной?
        _countCells = countCells;

        _prefabHeight = GetPrefabHeight();

        CreateCells();


        //for (int i = 0; i < _countCells; i++)
        //{
        //    Color? picked = colorSource.GetRandomColor();

        //    if (picked.HasValue)
        //    {
        //        AddCell(picked.Value);
        //    }
        //    else
        //    {
        //        Debug.LogWarning($"[MagicColumn] " +
        //            $"Недостаточно цветов для заполнения всех ячеек у " +
        //            $"{name}. Создано {i} из {_countCells}.");

        //        break;
        //    }
        //}
    }

    //заменить потом на фабрику Cell 
    public void CreateCells()
    {
        if (_cellPrefab == null || _colorSource == null)
        {
            Debug.LogError($"[MagicColumn] Отсутствует prefab или источник цветов у {name}.");
            return;
        }

        float currentY = transform.position.y;

        for (int i = 0; i < _countCells; i++)
        {
            Color? pickedColor = _colorSource.GetRandomColor();

            if (pickedColor.HasValue == false)
            {
                Debug.LogWarning($"[MagicColumn] " +
                    $"Недостаточно цветов для {name}. Создано {i} из {_countCells}.");

                break;
            }

            MagicCell cell = Instantiate(_cellPrefab, transform);

            cell.transform.localPosition = new Vector3(0, currentY, 0);
            cell.SetColor(pickedColor.Value);

            ClickHandler clickHandler = cell.GetComponent<ClickHandler>();

            if (clickHandler != null)
            {
                clickHandler.OnClicked += OnCellClicked;
            }

            _cellsStack.Push(cell);

            currentY += _prefabHeight;
        }
    }

    //public MagicCell AddCell(Color color)
    //{
    //    if (_cellPrefab == null)
    //    {
    //        Debug.LogError($"[MagicColumn] _cellPrefab не назначен у {name}.");

    //        return null;
    //    }

    //    MagicCell newCell = Instantiate(_cellPrefab, transform);

    //    // Локальная позиция: складываем высоту * текущий размер стека
    //    float localY = _cellsStack.Count * _prefabHeight;
    //    newCell.transform.localPosition = new Vector3(0f, localY, 0f);

    //    // Назначаем цвет
    //    newCell.SetColor(color);

    //    // Подписка на клик
    //    ClickHandler clickHandler = newCell.GetComponent<ClickHandler>();

    //    if (clickHandler != null)
    //    {
    //        clickHandler.OnClicked += OnCellClicked;
    //    }

    //    _cellsStack.Push(newCell);

    //    return newCell;
    //}

    private void OnCellClicked()
    {
        if (_cellsStack.Count == 0)
            return;

        MagicCell topCell = _cellsStack.Peek();

        if (_distributerMagic.IsCheckCellColor(_cellsStack.Peek()))
        {
            _cellsStack.Pop();

            CellDisplacing?.Invoke();
            _distributerMagic.DeliverMagicCell(topCell);
            topCell.Disable();
        }
    }

    private float GetPrefabHeight()
    {
        Renderer renderer = _cellPrefab.GetComponentInChildren<Renderer>();

        return renderer.bounds.size.y + _distanceBetweenCells;
    }
}
