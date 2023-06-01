using FactoryGame.Configuration;
using FactoryGame.Gameplay;
using FactoryGame.Services;
using FactoryGame.Services.GameData;
using System;
using System.Collections.Generic;

namespace FactoryGame.UI
{
    public class FactoryPopupModel : UIModel
    {
        public event Action<bool> ProductionStatusUpdated;
        public event Action<bool> ProductionAvailabilityUpdated;

        private IEnumerable<string> selectableItems;

        private ItemRecipes recipes;
        private ResourceFactoryComponent resourceFactory;
        private RecipeDisplayerModel recipeDisplayerModel;

        private GameDataService gameDataService;

        public IEnumerable<string> SelectableItems => selectableItems;
        public ItemRecipes Recipes => recipes;

        public FactoryPopupModel(
            ServiceLocator services,
            ItemRecipes recipes,
            ResourceFactoryComponent resourceFactory,
            IEnumerable<string> selectableItems)
                : base(services)
        {
            this.recipes = recipes;
            this.resourceFactory = resourceFactory;
            this.selectableItems = selectableItems;

            gameDataService = services.GetService<GameDataService>();

            resourceFactory.ProductionStatusUpdated += OnFactoryStatusUpdated;
        }

        public void UpdateProductionStatusAndAvailability()
        {
            if (resourceFactory.IsInProduction && resourceFactory.ActiveRecipe != null)
            {
                recipeDisplayerModel.SetRecipe(resourceFactory.ActiveRecipe);
            }

            ProductionStatusUpdated?.Invoke(resourceFactory.IsInProduction);
            ProductionAvailabilityUpdated?.Invoke(resourceFactory.CheckProductionAvailability(recipeDisplayerModel.SelectedRecipe));
        }

        public void StartProduction()
        {
            resourceFactory.StartProduction(recipeDisplayerModel.SelectedRecipe);
            ProductionStatusUpdated?.Invoke(true);
        }

        public void StopProduction()
        {
            resourceFactory.StopProduction();
            ProductionStatusUpdated?.Invoke(false);
        }

        public void AddRecipeDisplayerModel(RecipeDisplayerModel model)
        {
            recipeDisplayerModel = model;

            recipeDisplayerModel.RecipeResultUpdated += OnRecipeResultUpdated;
        }

        protected override void ReleaseInternal()
        {
            recipeDisplayerModel.RecipeResultUpdated -= OnRecipeResultUpdated;
            recipeDisplayerModel.Release();
            recipeDisplayerModel = null;

            resourceFactory.ProductionStatusUpdated -= OnFactoryStatusUpdated;
            resourceFactory = null;
        }

        private void OnFactoryStatusUpdated(bool status)
        {
            ProductionStatusUpdated?.Invoke(status);
        }

        private void OnRecipeResultUpdated()
        {
            ProductionAvailabilityUpdated?.Invoke(resourceFactory.CheckProductionAvailability(recipeDisplayerModel.SelectedRecipe));
        }
    }
}
