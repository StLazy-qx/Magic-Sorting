using Assets.Source.Scripts.Audio;
using Assets.Source.Scripts.UI.CanvasOption;
using Assets.Source.Scripts.UI.SoundView;
using Assets.Source.Scripts.Factory;
using UnityEngine;
using System;
using YG;
using UnityEngine.UI;

namespace Assets.Source.Scripts.EntryPoint
{
    public abstract class BasePlatformAdapter : MonoBehaviour
    {
        [Header("Canvases")]
        [SerializeField] protected CanvasMobileSetter MobileCanvas;
        [SerializeField] protected CanvasDesktopSetter DesktopCanvas;
        [Header("Store installation")]
        [SerializeField] protected StoreItemFactory ItemFactory;
        [SerializeField] protected Transform DesktopContent;
        [SerializeField] protected Transform MobileContent;
        [Header("Audio panels installation")]
        [SerializeField] private SoundSetter _soundSetter;
        [SerializeField] private VolumeSliderViewHandler _mobileAudioViewHandler;
        [SerializeField] private VolumeSliderViewHandler _desktopAudioViewHandler;
        //[Header("Loading Window Panel")]
        //[SerializeField] private Sprite _mobileloadindgImage;
        //[SerializeField] private Sprite _desktoploadingImage;

        //private LoadingWindow _loadingWindow;

        protected void InitializeBase(/*LoadingWindow loadingWindow*/)
        {
            ValidateRequiredObjects();
            MobileCanvas.Disable();
            DesktopCanvas.Disable();

            //_loadingWindow = loadingWindow
            //    ?? throw new ArgumentNullException(nameof(loadingWindow));

            if (YG2.envir.isMobile)
            {
                UseMobileMode();
                ItemFactory.SetContentTransform(MobileContent);
                _soundSetter.ApplyAudioHandler(_mobileAudioViewHandler);
                //_loadingWindow.SetParent(MobileCanvas.transform);
                //_loadingWindow.SetImage(_mobileloadindgImage);
                OnMobileSelected();
            }
            else
            {
                UseDesktopMode();
                ItemFactory.SetContentTransform(DesktopContent);
                _soundSetter.ApplyAudioHandler(_desktopAudioViewHandler);
                //_loadingWindow.SetParent(DesktopCanvas.transform);
                //_loadingWindow.SetImage(_desktoploadingImage);
                OnDesktopSelected();
            }
        }

        protected virtual void UseMobileMode()
        {
            MobileCanvas.Enable();
        }

        protected virtual void UseDesktopMode()
        {
            DesktopCanvas.Enable();
        }

        protected abstract void OnMobileSelected();

        protected abstract void OnDesktopSelected();

        private void ValidateRequiredObjects()
        {
            if (MobileCanvas == null)
                throw new ArgumentNullException(nameof(MobileCanvas));

            if (DesktopCanvas == null)
                throw new ArgumentNullException(nameof(DesktopCanvas));

            if (ItemFactory == null)
                throw new ArgumentNullException(nameof(ItemFactory));

            if (MobileContent == null)
                throw new ArgumentNullException(nameof(MobileContent));

            if (DesktopContent == null)
                throw new ArgumentNullException(nameof(DesktopContent));

            //if (_mobileloadindgImage == null)
            //    throw new ArgumentNullException(nameof(_mobileloadindgImage));

            //if (_desktoploadingImage == null)
            //    throw new ArgumentNullException(nameof(_desktoploadingImage));
        }
    }
}
