namespace FactoryGame.UI
{
    public class QueueItemSelectorController : UIController<QueueItemSelectorModel, QueueItemSelectorViewBase>
    {
        public QueueItemSelectorController(QueueItemSelectorModel model, QueueItemSelectorViewBase view)
            : base(model, view)
        {
            this.model = model;
            this.view = view;

            AddDisplayerModel();

            model.SelectNextItem();
            view.NextItemSelectPressed += OnViewButtonPressed;
        }

        private void AddDisplayerModel()
        {
            var resourceDisplayerModel = new ResourceDisplayerModel(model.Services);
            var resourceDisplayerView = view.ResourceDisplayerView;
            var resourceDisplayerController = new ResourceDisplayerController(resourceDisplayerModel, resourceDisplayerView);

            model.AddResourceDisplayerModel(resourceDisplayerModel);
        }

        private void OnViewButtonPressed()
        {
            model.SelectNextItem();
        }
    }
}
