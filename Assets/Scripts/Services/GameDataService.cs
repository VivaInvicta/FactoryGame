using FactoryGame.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FactoryGame.Services.GameData
{
    [CreateAssetMenu(fileName = nameof(GameDataService), menuName = "Services/" + nameof(GameDataService))]
    public class GameDataService : Service
    {
        [SerializeField]
        private ItemsCollection itemsConfiguration;

        [SerializeField]
        private string saveKey = "resources";

        private ResourcesData resources;

        public ResourcesData Resources => resources;
        public IEnumerable<ItemConfig> ItemConfigurations => itemsConfiguration.Items;

        protected override void InitializeInternal()
        {
            GetResourcesFromSave();
        }

        public void Save()
        {
            var serializedResources = JsonConvert.SerializeObject(resources.ResourcesDictionary);

            PlayerPrefs.SetString(saveKey, serializedResources);
            PlayerPrefs.Save();

            Debug.Log(serializedResources);
        }

        public void ClearSave()
        {
            PlayerPrefs.DeleteKey(saveKey);
            PlayerPrefs.Save();
        }

        public ItemConfig GetItemConfigById(string id)
        {
            return itemsConfiguration.Items.FirstOrDefault(item => item.Id == id);
        }

        private void GetResourcesFromSave()
        {
            var savedResources = PlayerPrefs.GetString(saveKey);
            resources = new ResourcesData();

            if (!string.IsNullOrEmpty(savedResources) && savedResources != "{}")
            {
                resources.ResourcesDictionary = JsonConvert.DeserializeObject<Dictionary<string, int>>(savedResources);
            }
        }
    }
}
