using FactoryGame.Services;

namespace FactoryGame.UI
{
    public class ResourceMinerPopupController : UIController<ResourceMinerPopupModel, ResourceMinerPopupView>
    {
        private QueueItemSelectorController itemSelectorController;

        public ResourceMinerPopupController(ResourceMinerPopupModel model, ResourceMinerPopupView view)
            : base(model, view)
        {
            itemSelectorController = CreateItemSelector();

            model.MiningStatusUpdated += OnMiningStatusChanged;

            view.StartButtonPressed += OnStartButtonPressed;
            view.StopButtonPressed += OnStopButtonPressed;
            view.BackgroundPressed += OnViewBackgroundPressed;

            model.UpdateMiningStatus();
        }

        private QueueItemSelectorController CreateItemSelector()
        {
            var itemSelectorModel = new QueueItemSelectorModel(model.Services, model.AvailableResources);
            var itemSelectorController = new QueueItemSelectorController(itemSelectorModel, view.ItemSelectorView);

            model.SetSelectorModel(itemSelectorModel);

            return itemSelectorController;
        }

        private void OnViewBackgroundPressed()
        {
            model.Services.GetService<UIService>().DestroyUIElementView(view);
        }

        private void OnStartButtonPressed()
        {
            model.StartMining();
        }

        private void OnStopButtonPressed()
        {
            model.StopMining();
        }

        private void OnMiningStatusChanged(bool status)
        {
            if (status)
            {
                view.ShowStopButton();
                itemSelectorController.SetViewInteractable(false);
            }
            else
            {
                view.ShowStartButton();
                itemSelectorController.SetViewInteractable(true);
            }
        }
    }
}