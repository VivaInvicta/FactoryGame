using System.Collections.Generic;

namespace FactoryGame.UI
{
    public class StorageControler : UIController<StorageModel, StorageView>
    {
        private Dictionary<ResourceDisplayerModel, ResourceDisplayerController> resourceDisplayers
            = new Dictionary<ResourceDisplayerModel, ResourceDisplayerController>();

        public StorageControler(StorageModel model, StorageView view)
            : base(model, view)
        {
            model.ResourceDisplayerAdded += OnResourceDisplayerAdded;
            model.ResourceDisplayerRemoved += OnResourceDisplayerRemoved;

            model.InitResources();
        }

        private void OnResourceDisplayerAdded(ResourceDisplayerModel displayerModel)
        {
            CreateResourceDisplayer(displayerModel);
        }

        private void OnResourceDisplayerRemoved(ResourceDisplayerModel displayerModel)
        {
            var displayerController = resourceDisplayers[displayerModel];

            model.UIService.DestroyUIElementView(displayerController.View);
        }

        private void CreateResourceDisplayer(ResourceDisplayerModel displayerModel)
        {
            var displayerView = model.UIService.CreateUIElementView(view.ResourceDisplayerViewPrefab);
            view.AddResourceDisplayer(displayerView);

            var displayerController = new ResourceDisplayerController(displayerModel, displayerView);

            resourceDisplayers.Add(displayerModel, displayerController);
        }
    }
}