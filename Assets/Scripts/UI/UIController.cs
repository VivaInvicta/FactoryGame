namespace FactoryGame.UI
{
    public abstract class UIController<M, V>
                    where M : UIModel
                    where V : UIView
    {
        public M Model => model;
        public V View => view;

        protected M model;
        protected V view;

        public UIController(M model, V view)
        {
            this.model = model;
            this.view = view;

            view.OnClose += OnClose;
        }

        public void SetViewInteractable(bool interactable)
        {
            view.SetInteractable(interactable);
        }

        private void OnClose() => model.Release();
    }
}