using FactoryGame.Services;
using FactoryGame.UI;
using UnityEngine;

namespace FactoryGame.Gameplay
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class PopupBuildingComponent<M, V, C> : BuildingComponent
        where M : UIModel
        where V : UIView
        where C : UIController<M, V>
    {
        [SerializeField]
        private UIViewPlacementComponent popupPlacement;

        [SerializeField]
        private V popupViewPrefab;

        private UIService uiService;

        protected M model;
        protected V view;

        protected override void InitializeInternal()
        {
            uiService = services.GetService<UIService>();
        }

        protected abstract M CreateModel();
        protected abstract C CreateController(M model, V view);


        private void OnMouseDown()
        {
            if (popupPlacement.AttachedView == null)
                CreatePopup();
        }

        private void CreatePopup()
        {
            var popupModel = CreateModel();

            var popupPosition = popupPlacement.ScreenPosition;
            var popupView = uiService.CreateUIElementView(popupViewPrefab, popupPosition);

            CreateController(popupModel, popupView);

            popupPlacement.AttachedView = popupView;
        }

    }
}