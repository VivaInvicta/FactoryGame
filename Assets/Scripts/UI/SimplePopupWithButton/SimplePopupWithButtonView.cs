using System;
using UnityEngine;
using UnityEngine.UI;

namespace FactoryGame.UI
{
    public class SimplePopupWithButtonView : UIView
    {
        public event Action ButtonClicked;

        [SerializeField]
        private Button button;

        private void OnEnable()
        {
            button.onClick.AddListener(() => ButtonClicked?.Invoke());
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}