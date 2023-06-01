using FactoryGame.Configuration;
using FactoryGame.Services.GameData;
using FactoryGame.UI;
using System;
using UnityEngine;

namespace FactoryGame.Gameplay
{
    public class ResourceFactoryComponent : BuildingComponent
    {
        public event Action<bool> ProductionStatusUpdated;

        [SerializeField]
        private float productionDuration = 1f;

        [SerializeField]
        private int materialsConsumeCount = 1;

        [SerializeField]
        private int resultCount = 1;

        private string targetItem = null;

        private float timePassed;
        private bool productionStarted;

        private ResourcesData resourcesData;
        private ItemRecipe activeRecipe;

        public ItemRecipe ActiveRecipe => activeRecipe;
        public bool IsInProduction => productionStarted;
        public string TargetItem => targetItem;

        protected override void InitializeInternal()
        {
            resourcesData = services.GetService<GameDataService>().Resources;
        }

        protected override void ProcessInternal(float deltaTime)
        {
            if (productionStarted)
            {
                timePassed += deltaTime;

                if (timePassed > productionDuration)
                {
                    timePassed = 0;
                    ProductResource();
                }
            }
            else
            {
                timePassed = 0;
            }
        }

        public void StartProduction(string targetResource)
        {
            targetItem = targetResource;

            SetProductionStarted();
        }

        public void StartProduction(ItemRecipe recipe)
        {
            if (recipe == null)
                return;

            activeRecipe = recipe;

            SetProductionStarted();
        }

        public void StopProduction()
        {
            productionStarted = false;
            ProductionStatusUpdated?.Invoke(false);

            activeRecipe = null;
            targetItem = null;
        }

        public bool CheckProductionAvailability(ItemRecipe recipe)
        {
            if (recipe == null)
                return false;

            foreach (var material in recipe.Materials)
            {
                if (resourcesData.GetResourceCountById(material) < materialsConsumeCount)
                {
                    return false;
                }
            }
            return true;
        }

        private void SetProductionStarted()
        {
            productionStarted = true;
            ProductionStatusUpdated?.Invoke(true);
        }

        private void ProductResource()
        {
            if (activeRecipe != null)
            {
                if (CheckProductionAvailability(activeRecipe))
                {
                    ConsumeMaterials();
                    resourcesData.AddResource(activeRecipe.Result, resultCount);

                    return;
                }
                else
                {
                    StopProduction();

                    return;
                }
            }

            resourcesData.AddResource(targetItem, resultCount);
        }

        private void ConsumeMaterials()
        {
            foreach (var material in activeRecipe.Materials)
            {
                resourcesData.ConsumeResource(material, materialsConsumeCount);
            }
        }
    }
}