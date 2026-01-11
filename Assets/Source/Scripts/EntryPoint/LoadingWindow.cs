using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Source.Scripts.EntryPoint
{
    public class LoadingWindow : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public static LoadingWindow Instance { get; private set; }

        private void Awake()
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
            Hide();
        }

        public void SetImage(Sprite sprite)
        {
            if (_image == null)
                throw new InvalidOperationException("Image is null");

            _image.sprite = sprite;
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent, false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}