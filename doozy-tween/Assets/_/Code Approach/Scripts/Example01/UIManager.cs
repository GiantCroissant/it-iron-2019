namespace ItIron2019.DoozyTween.Runtime.Example01
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    using DG.Tweening;

    public class UIManager : MonoBehaviour
    {
        public UnityEngine.UI.Button button;

        public RectTransform view;
        private bool _viewOnscreen;
        private Vector2 _startingPos;

        void Start()
        {
            _startingPos = new Vector2(view.anchoredPosition.x, view.anchoredPosition.y);
            button.onClick.AddListener(HandleButtonClick);
        }

        private void HandleButtonClick()
        {
            _viewOnscreen = !_viewOnscreen;
            if (_viewOnscreen)
            {
                view.DOAnchorPos(Vector2.zero, 2.0f, true);
            }
            else
            {
                view.DOAnchorPos(_startingPos, 2.0f, true);
            }
        }
    }
}
