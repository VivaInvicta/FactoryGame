using FactoryGame.Gameplay;
using FactoryGame.Services;
using System;
using System.Collections.Generic;

namespace FactoryGame.UI
{
    public class ResourceMinerPopupModel : UIModel
    {
        public event Action<bool> MiningStatusUpdated;

        private IEnumerable<string> availableResources;
        private QueueItemSelectorModel itemSelectorModel;

        private ResourceFactoryComponent minerComponent;

        public IEnumerable<string> AvailableResources => availableResources;

        public ResourceMinerPopupModel(
               ServiceLocator services,
               IEnumerable<string> availableResources,
               ResourceFactoryComponent minerComponent)
                    : base(services)
        {
            this.availableResources = availableResources;
            this.minerComponent = minerComponent;

            minerComponent.ProductionStatusUpdated += OnMinerProductionStatusUpdated;
        }

        public void UpdateMiningStatus()
        {
            if (minerComponent.IsInProduction)
            {
                SetSelectorToCurrentItem();
            }
            MiningStatusUpdated?.Invoke(minerComponent.IsInProduction);
        }

        private void SetSelectorToCurrentItem()
        {
            itemSelectorModel.SkipToItem(minerComponent.TargetItem);
        }

        public void StartMining()
        {
            minerComponent.StartProduction(itemSelectorModel.SelectedItem.config.Id);
        }

        public void StopMining()
        {
            minerComponent.StopProduction();
        }

        private void OnMinerProductionStatusUpdated(bool status)
        {
            MiningStatusUpdated?.Invoke(status);
        }

        public void SetSelectorModel(QueueItemSelectorModel model)
        {
            itemSelectorModel = model;
            itemSelectorModel.SelectNextItem();
        }

        protected override void ReleaseInternal()
        {
            if (itemSelectorModel != null)
            {
                itemSelectorModel.Release();
                itemSelectorModel = null;
            }

            availableResources = null;

            if (minerComponent != null)
            {
                minerComponent.ProductionStatusUpdated -= OnMinerProductionStatusUpdated;
                minerComponent = null;
            }
        }
    }
}