using FactoryGame.Services;

namespace FactoryGame.UI
{
    public class FactoryPopupController : UIController<FactoryPopupModel, FactoryPopupView>
    {
        private RecipeDisplayerController recipeDisplayerController;

        public FactoryPopupController(FactoryPopupModel model, FactoryPopupView view)
            : base(model, view)
        {
            recipeDisplayerController = CreateRecipeDisplayerController();

            model.ProductionStatusUpdated += OnProductionStatusUpdated;
            model.ProductionAvailabilityUpdated += OnProductionAvailablityUpdated;

            view.StartButtonPressed += OnStartButtonPressed;
            view.StopButtonPressed += OnStopButtonPressed;
            view.BackgroundPressed += OnViewBackgroundPressed;

            model.UpdateProductionStatusAndAvailability();
        }

        private RecipeDisplayerController CreateRecipeDisplayerController()
        {
            var recipeDisplayerModel = new RecipeDisplayerModel(model.Services, model.Recipes, model.SelectableItems);
            var recipeDisplayerController = new RecipeDisplayerController(recipeDisplayerModel, view.RecipeDisplayer);

            model.AddRecipeDisplayerModel(recipeDisplayerModel);

            return recipeDisplayerController;
        }

        private void OnViewBackgroundPressed()
        {
            model.Services.GetService<UIService>().DestroyUIElementView(view);
        }

        private void OnStartButtonPressed()
        {
            model.StartProduction();
        }

        private void OnStopButtonPressed()
        {
            model.StopProduction();
        }

        private void OnProductionStatusUpdated(bool status)
        {
            if (status)
            {
                view.ShowStopButton();
                SetViewsInteractable(false);
            }
            else
            {
                view.ShowStartButton();
                SetViewsInteractable(true);
            }
        }

        private void OnProductionAvailablityUpdated(bool availability)
        {
            view.SetInteractable(availability);
        }

        private void SetViewsInteractable(bool isInteractable)
        {
            SetViewInteractable(isInteractable);
            recipeDisplayerController.SetViewInteractable(isInteractable);
        }
    }
}
