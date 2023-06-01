using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FactoryGame.Configuration
{
    [CreateAssetMenu]
    public class MarketConfig : ScriptableObject
    {
        [SerializeField]
        private ItemRewardConfig[] itemRewardConfigs;

        public IEnumerable<string> GetItemsList() => itemRewardConfigs.Select(config => config.Item);

        public ItemRewardConfig GetItemRewardConfig(string item) => itemRewardConfigs.First(config => config.Item == item);
    }

    [Serializable]
    public class ItemRewardConfig
    {
        [SerializeField]
        private string item;

        [SerializeField]
        private string reward;

        [SerializeField]
        private int rewardCost;

        [SerializeField]
        private int rewardCount;

        public string Item => item;
        public string Reward => reward;
        public int RewardCount => rewardCount;
        public int RewardCost => rewardCost;
    }
}