using System;
using UnityEngine;
using UnityEngine.UI;

namespace FactoryGame.UI
{
    public class MarketPopupView : UIView, ISinglePopupView
    {
        public event Action SellButtonPressed;
        public event Action BackgroundButtonPressed;

        [SerializeField]
        private QueueItemSelectorViewBase itemSelector;

        [SerializeField]
        private Button sellButton;

        [SerializeField]
        private Button backgroundButton;

        [SerializeField]
        private Image rewardIcon;

        [SerializeField]
        private Text rewardCountText;

        [SerializeField]
        private Text priceText;

        public QueueItemSelectorViewBase ItemSelector => itemSelector;

        public void SetPrice(int price)
        {
            priceText.text = $"-{price}";
        }

        public void SetRewardCount(int count)
        {
            rewardCountText.text = $"+{count}";
        }

        public void SetRewardIcon(Sprite icon)
        {
            rewardIcon.sprite = icon;
        }

        protected override void SetInteractableInternal(bool interactable)
        {
            sellButton.interactable = interactable;
        }

        private void OnEnable()
        {
            sellButton.onClick.AddListener(OnSellButtonPressed);
            backgroundButton.onClick.AddListener(OnBackgroundButtonPressed);
        }

        private void OnDisable()
        {
            sellButton.onClick.RemoveListener(OnSellButtonPressed);
            backgroundButton.onClick.RemoveListener(OnBackgroundButtonPressed);
        }

        private void OnBackgroundButtonPressed()
        {
            BackgroundButtonPressed?.Invoke();
        }

        private void OnSellButtonPressed()
        {
            SellButtonPressed?.Invoke();
        }
    }
}