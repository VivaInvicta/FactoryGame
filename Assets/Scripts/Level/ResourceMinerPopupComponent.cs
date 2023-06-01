using FactoryGame.UI;
using UnityEngine;

namespace FactoryGame.Gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public class ResourceMinerPopupComponent : PopupBuildingComponent
        <ResourceMinerPopupModel, ResourceMinerPopupView, ResourceMinerPopupController>
    {
        [SerializeField]
        private ResourceFactoryComponent resourceMiner;

        [SerializeField]
        private string[] availableResources;

        protected override ResourceMinerPopupModel CreateModel()
            => new ResourceMinerPopupModel(services, availableResources, resourceMiner);

        protected override ResourceMinerPopupController CreateController(ResourceMinerPopupModel model, ResourceMinerPopupView view)
            => new ResourceMinerPopupController(model, view);
    }
}