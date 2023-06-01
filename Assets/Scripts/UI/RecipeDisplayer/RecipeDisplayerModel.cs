using FactoryGame.Configuration;
using FactoryGame.Services;
using FactoryGame.Services.GameData;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FactoryGame.UI
{
    public class RecipeDisplayerModel : UIModel
    {
        public event Action RecipeResultUpdated;

        private List<QueueItemSelectorModel> materialSelectorModels = new List<QueueItemSelectorModel>();
        private ResourceDisplayerModel resultDisplayerModel;
        private IEnumerable<string> selectableItem;

        private ItemRecipes recipes;
        private ItemRecipe selectedRecipe;
        private GameDataService gameDataService;
        private ResourcesData resourcesData;

        public IEnumerable<string> SelectableItems => selectableItem;
        public ItemRecipe SelectedRecipe => selectedRecipe;

        public RecipeDisplayerModel(ServiceLocator services,
            ItemRecipes recipes,
            IEnumerable<string> selectableResources)
                : base(services)
        {
            gameDataService = services.GetService<GameDataService>();
            resourcesData = gameDataService.Resources;

            this.recipes = recipes;
            selectableItem = selectableResources;
        }

        public void AddResultDisplayerModel(ResourceDisplayerModel displayerModel)
        {
            resultDisplayerModel = displayerModel;
        }

        public void AddSelectorModel(QueueItemSelectorModel model)
        {
            materialSelectorModels.Add(model);

            model.Updated += OnSelectorsUpdated;
        }

        public void SetRecipe(ItemRecipe recipe)
        {
            for (var i = 0; i < recipe.Materials.Count(); i++)
            {
                materialSelectorModels.ElementAt(i).SkipToItem(recipe.Materials.ElementAt(i));
            }
        }

        protected override void ReleaseInternal()
        {
            foreach (var model in materialSelectorModels)
            {
                model.Updated -= OnSelectorsUpdated;
                model.Release();
            }

            resultDisplayerModel.Release();
            resultDisplayerModel = null;

            resourcesData = null;
            materialSelectorModels = null;
            gameDataService = null;
        }


        private void OnSelectorsUpdated()
        {
            var selectedItemIds = materialSelectorModels.Select(item => item.SelectedItem.config.Id);
            selectedRecipe = GetMatchedRecipe(selectedItemIds);

            RecipeResultUpdated?.Invoke();

            if (selectedRecipe != null)
            {
                resultDisplayerModel.SetDisplayedResource(selectedRecipe.Result);
            }
        }

        private ItemRecipe GetMatchedRecipe(IEnumerable<string> materials)
        {
            foreach (var recipe in recipes.Recipes)
            {
                var isMatched = true;

                foreach (var material in recipe.Materials)
                {
                    if (!materials.Contains(material))
                    {
                        isMatched = false;
                        break;
                    }
                }

                if (isMatched)
                {
                    return recipe;
                }
            }

            return null;
        }
    }
}