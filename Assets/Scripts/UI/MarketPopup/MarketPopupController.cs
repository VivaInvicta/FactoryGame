using FactoryGame.Services;

namespace FactoryGame.UI
{
    public class MarketPopupController : UIController<MarketPopupModel, MarketPopupView>
    {
        public MarketPopupController(MarketPopupModel model, MarketPopupView view)
            : base(model, view)
        {
            model.RewardUpdated += OnRewardConfigUpdated;
            model.SellAvailabilityUpdated += OnSellAvailabilityUpdated;

            view.SellButtonPressed += OnSellButtonPressed;
            view.BackgroundButtonPressed += OnBackgroundButtonPressed;

            CreateSelector();
        }

        private void CreateSelector()
        {
            var selectorModel = new QueueItemSelectorModel(model.Services, model.SellableResources);
            model.AddSelectorModel(selectorModel);

            var selectorController = new QueueItemSelectorController(selectorModel, view.ItemSelector);
        }

        private void OnSellButtonPressed()
        {
            model.SellSelectedResource();
        }

        private void OnSellAvailabilityUpdated(bool availability)
        {
            view.SetInteractable(availability);
        }

        private void OnBackgroundButtonPressed()
        {
            model.Services.GetService<UIService>().DestroyUIElementView(view);
        }

        private void OnRewardConfigUpdated()
        {
            var newConfig = model.RewardConfig;

            view.SetPrice(newConfig.RewardCost);
            view.SetRewardCount(newConfig.RewardCount);
            view.SetRewardIcon(model.GetRewardIcon());
        }
    }
}