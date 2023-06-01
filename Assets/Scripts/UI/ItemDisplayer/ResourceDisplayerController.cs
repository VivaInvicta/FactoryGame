namespace FactoryGame.UI
{
    public class ResourceDisplayerController : UIController<ResourceDisplayerModel, ResourceDisplayerView>
    {
        public ResourceDisplayerController(ResourceDisplayerModel model, ResourceDisplayerView view)
            : base(model, view)
        {
            model.DisplayedResourceUpdated += OnDisplayedResourceUpdated;
        }

        private void OnDisplayedResourceUpdated()
        {
            view.SetItemImage(model.DisplayedResource.config.Icon);
            view.SetCount(model.DisplayedResource.count);
        }
    }
}