using FactoryGame.Services;
using FactoryGame.Services.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FactoryGame.UI
{
    public class StorageModel : UIModel
    {
        public Action<ResourceDisplayerModel> ResourceDisplayerAdded;
        public Action<ResourceDisplayerModel> ResourceDisplayerRemoved;

        private ResourcesData resourcesData;
        private UIService uiService;
        private GameDataService gameDataService;

        private List<ResourceDisplayerModel> resourceDisplayerModels = new List<ResourceDisplayerModel>();

        public UIService UIService => uiService;

        public StorageModel(ServiceLocator services)
            : base(services)
        {
            resourcesData = services.GetService<GameDataService>().Resources;
            uiService = services.GetService<UIService>();
            gameDataService = services.GetService<GameDataService>();

            resourcesData.OnUpdate += OnResourceDataUpdated;
        }

        protected override void ReleaseInternal()
        {
            resourcesData.OnUpdate -= OnResourceDataUpdated;

            resourcesData = null;
            uiService = null;
            gameDataService = null;
        }

        public void InitResources()
        {
            var itemIds = gameDataService.ItemConfigurations.Select(configuration => configuration.Id);

            foreach (var itemId in itemIds)
            {
                var resourceCount = resourcesData.GetResourceCountById(itemId);
                if (resourceCount > 0)
                {
                    AddResourceDisplayer(itemId);
                }
            }
        }

        private void OnResourceDataUpdated(string resourceId, int _)
        {
            if (!TryGetMatchedResourceDisplayer(resourceId, out var _))
            {
                AddResourceDisplayer(resourceId);
            }
        }

        private void AddResourceDisplayer(string resourceId)
        {
            var resourceConfig = gameDataService.GetItemConfigById(resourceId);

            if (!resourceConfig.ShowInStorage)
                return;

            var displayerModel = new ResourceDisplayerModel(services);
            displayerModel.OutOfResource += OnOutOfResource;

            ResourceDisplayerAdded?.Invoke(displayerModel);

            displayerModel.SetDisplayedResource(resourceId);
            resourceDisplayerModels.Add(displayerModel);
        }

        private bool TryGetMatchedResourceDisplayer(string resourceId, out ResourceDisplayerModel displayerModel)
        {
            foreach (var resourceDisplayerModel in resourceDisplayerModels)
            {
                if (resourceDisplayerModel.DisplayedResource.config.Id == resourceId)
                {
                    displayerModel = resourceDisplayerModel;
                    return true;
                }
            }
            displayerModel = null;
            return false;
        }

        private void OnOutOfResource(string resourceId)
        {
            if (TryGetMatchedResourceDisplayer(resourceId, out var displayerModel))
            {
                displayerModel.OutOfResource -= OnOutOfResource;
                resourceDisplayerModels.Remove(displayerModel);

                ResourceDisplayerRemoved?.Invoke(displayerModel);
            }
        }
    }
}