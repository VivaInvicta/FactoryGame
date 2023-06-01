using FactoryGame.Configuration;
using FactoryGame.Services;
using FactoryGame.Services.GameData;
using System;
using System.Collections;

namespace FactoryGame.UI
{
    public class ResourceDisplayerModel : UIModel
    {
        public event Action DisplayedResourceUpdated;
        public event Action<string> OutOfResource;

        private ResourcesData resourcesData;
        private GameDataService gameDataService;

        private (ItemConfig config, int count) displayedResource;

        public (ItemConfig config, int count) DisplayedResource => displayedResource;

        public ResourceDisplayerModel(ServiceLocator services)
            : base(services)
        {
            gameDataService = services.GetService<GameDataService>();
            resourcesData = gameDataService.Resources;

            resourcesData.OnUpdate += OnResourcesDataUpdated;
        }

        public void SetDisplayedResource(string resourceId)
        {
            var itemConfig = gameDataService.GetItemConfigById(resourceId);
            var itemCount = resourcesData.GetResourceCountById(resourceId);

            displayedResource = (itemConfig, itemCount);

            DisplayedResourceUpdated?.Invoke();
        }

        protected override void ReleaseInternal()
        {
            resourcesData.OnUpdate -= OnResourcesDataUpdated;

            resourcesData = null;
            gameDataService = null;
        }

        private void OnResourcesDataUpdated(string id, int newCount)
        {
            if (displayedResource.config != null && displayedResource.config.Id == id)
            {
                displayedResource = (displayedResource.config, newCount);
                DisplayedResourceUpdated?.Invoke();

                if (newCount == 0)
                {
                    OutOfResource?.Invoke(id);
                }
            }
        }
    }
}