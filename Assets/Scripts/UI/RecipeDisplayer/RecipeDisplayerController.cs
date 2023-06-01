namespace FactoryGame.UI
{
    public class RecipeDisplayerController : UIController<RecipeDisplayerModel, RecipeDisplayerView>
    {
        public RecipeDisplayerController(RecipeDisplayerModel model, RecipeDisplayerView view)
            : base(model, view)
        {
            CreateMaterialSelectors();
            AddResultDisplayer();

            model.RecipeResultUpdated += OnModelUpdated;
        }

        private void AddResultDisplayer()
        {
            var resourceDisplayerModel = new ResourceDisplayerModel(model.Services);
            var resourceDisplayerView = view.ResultDisplayerView;
            var resourceDisplayerController = new ResourceDisplayerController(resourceDisplayerModel, resourceDisplayerView);

            model.AddResultDisplayerModel(resourceDisplayerModel);
        }

        private void CreateMaterialSelectors()
        {
            foreach (var materialSelectorView in view.MaterialSelectors)
            {
                var materialSelectorModel = new QueueItemSelectorModel(model.Services, model.SelectableItems);
                var materialSelectorController = new QueueItemSelectorController(materialSelectorModel, materialSelectorView);

                model.AddSelectorModel(materialSelectorModel);
            }
        }

        private void OnModelUpdated()
        {
            var recipe = model.SelectedRecipe;

            view.SetResultEnabled(recipe != null);
        }
    }
}