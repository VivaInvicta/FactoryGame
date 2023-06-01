using FactoryGame.Configuration;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace FactoryGame.UI
{
    [RequireComponent(typeof(Button))]
    public class SingleButtonItemSelectorView : QueueItemSelectorViewBase
    {
        public override event Action NextItemSelectPressed;

        [SerializeField]
        private Button button;

        [SerializeField]
        private ResourceDisplayerView resourceDisplayerView;

        public override ResourceDisplayerView ResourceDisplayerView => resourceDisplayerView;

        protected override void SetInteractableInternal(bool isInteractable)
        {
            button.interactable = isInteractable;
        }

        private void OnEnable() => button.onClick.AddListener(OnButtonPressed);

        private void OnDisable() => button.onClick.RemoveListener(OnButtonPressed);

        private void OnButtonPressed() => NextItemSelectPressed?.Invoke();
    }
}
