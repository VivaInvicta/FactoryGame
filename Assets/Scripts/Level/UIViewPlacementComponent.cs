using UnityEngine;

namespace FactoryGame.Gameplay
{
    public class UIViewPlacementComponent : MonoBehaviour
    {
        public Vector3 ScreenPosition => Camera.main.WorldToScreenPoint(transform.position);

        public UIView AttachedView
        {
            get
            {
                return uiView;
            }
            set
            {
                if (uiView != null)
                    uiView.OnClose -= OnViewClosed;

                if (value != null)
                    value.OnClose += OnViewClosed;

                uiView = value;
            }
        }

        private UIView uiView;

        private void OnViewClosed()
        {
            AttachedView = null;
        }
    }
}
