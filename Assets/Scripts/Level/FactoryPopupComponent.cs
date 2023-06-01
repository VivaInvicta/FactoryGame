using FactoryGame.Configuration;
using FactoryGame.UI;
using UnityEngine;

namespace FactoryGame.Gameplay
{
    public class FactoryPopupComponent : PopupBuildingComponent
        <FactoryPopupModel, FactoryPopupView, FactoryPopupController>
    {
        [SerializeField]
        private ResourceFactoryComponent resourceFactory;

        [SerializeField]
        private string[] availableResources;

        [SerializeField]
        private ItemRecipes availableRecipes;

        protected override FactoryPopupModel CreateModel()
            => new FactoryPopupModel(services, availableRecipes, resourceFactory, availableResources);

        protected override FactoryPopupController CreateController(FactoryPopupModel model, FactoryPopupView view)
            => new FactoryPopupController(model, view);
    }
}
