namespace ItIron2019.DoozyTween.Runtime.Example02
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    using DG.Tweening;

    public class UIManager : MonoBehaviour
    {
        public UnityEngine.UI.Button button;

        public UnityEngine.UI.Toggle linearToggle;
        public UnityEngine.UI.Toggle inSineToggle;
        public UnityEngine.UI.Toggle inQuadToggle;
        public UnityEngine.UI.Toggle inCubicToggle;

        public RectTransform view;
        private bool _viewOnscreen;
        private Vector2 _startingPos;

        private Ease _currentEase;
        
        void Start()
        {
            _startingPos = new Vector2(view.anchoredPosition.x, view.anchoredPosition.y);
            _currentEase = Ease.Linear;
            button.onClick.AddListener(HandleButtonClick);
        }

        private void HandleButtonClick()
        {
            _viewOnscreen = !_viewOnscreen;
            if (_viewOnscreen)
            {
                view
                    .DOAnchorPos(Vector2.zero, 2.0f, true)
                    .SetEase(_currentEase);
            }
            else
            {
                view.DOAnchorPos(_startingPos, 2.0f, true)
                    .SetEase(_currentEase);
            }
        }

        public void HandleToggleLinear()
        {
            if (linearToggle.isOn)
            {
                _currentEase = Ease.Linear;
            }
        }

        public void HandleToggleInSine()
        {
            if (inSineToggle.isOn)
            {
                _currentEase = Ease.InSine;
            }
        }

        public void HandleToggleInQuad()
        {
            if (inQuadToggle.isOn)
            {
                _currentEase = Ease.InQuad;
            }
        }
        
        public void HandleToggleInCubic()
        {
            if (inCubicToggle.isOn)
            {
                _currentEase = Ease.InCubic;
            }
        }
    }
}
