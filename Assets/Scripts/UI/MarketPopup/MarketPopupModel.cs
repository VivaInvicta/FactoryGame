using FactoryGame.Configuration;
using FactoryGame.Services;
using FactoryGame.Services.GameData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FactoryGame.UI
{
    public class MarketPopupModel : UIModel
    {
        public event Action<bool> SellAvailabilityUpdated;
        public event Action RewardUpdated;

        private QueueItemSelectorModel selectorModel;
        private ResourcesData resources;
        private GameDataService gameDataService;

        private MarketConfig marketConfig;
        private IEnumerable<string> sellableResources;

        private ItemRewardConfig rewardConfig;

        public IEnumerable<string> SellableResources => sellableResources;
        public ItemRewardConfig RewardConfig => rewardConfig;

        public MarketPopupModel(ServiceLocator services, MarketConfig marketConfig)
            : base(services)
        {
            gameDataService = services.GetService<GameDataService>();
            resources = gameDataService.Resources;

            this.marketConfig = marketConfig;
            sellableResources = marketConfig.GetItemsList();
        }

        public void SellSelectedResource()
        {
            resources.ConsumeResource(rewardConfig.Item, rewardConfig.RewardCost);
            resources.AddResource(rewardConfig.Reward, rewardConfig.RewardCount);
        }

        public void AddSelectorModel(QueueItemSelectorModel model)
        {
            selectorModel = model;

            model.Updated += OnSelectorUpdated;
        }

        public Sprite GetRewardIcon()
        {
            var rewardItemConfig = gameDataService.GetItemConfigById(rewardConfig.Reward);
            return rewardItemConfig.Icon;
        }

        protected override void ReleaseInternal()
        {
            if (selectorModel != null)
            {
                selectorModel.Release();
                selectorModel.Updated -= OnSelectorUpdated;
            }
            selectorModel = null;
            resources = null;
            rewardConfig = null;
        }

        private void OnSelectorUpdated()
        {
            var selectedItemId = selectorModel.SelectedItem.config.Id;
            rewardConfig = marketConfig.GetItemRewardConfig(selectedItemId);

            var selectedItemSellCost = rewardConfig.RewardCost;
            var isSellAvailable = resources.GetResourceCountById(selectedItemId) >= selectedItemSellCost;

            SellAvailabilityUpdated?.Invoke(isSellAvailable);
            RewardUpdated?.Invoke();
        }
    }
}