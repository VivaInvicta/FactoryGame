using FactoryGame.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FactoryGame.Services
{
    [CreateAssetMenu(fileName = nameof(UIService), menuName = "Services/" + nameof(UIService))]
    public class UIService : Service
    {
        [SerializeField]
        private Canvas canvasPrefab;

        [SerializeField]
        private EventSystem eventSystemPrefab;

        private Canvas canvas;

        private List<UIView> openedUIElements;

        protected override void InitializeInternal()
        {
            canvas = Instantiate(canvasPrefab);
            Instantiate(eventSystemPrefab);
        }

        public T CreateUIElementView<T>(T elementViewPrefab) where T : UIView
        {
            var view = Instantiate(elementViewPrefab, canvas.transform);

            if (view is ISinglePopupView)
                CloseOtherPopups();

            if (openedUIElements == null)
                openedUIElements = new List<UIView>();

            openedUIElements.Add(view);

            return view;
        }

        public T CreateUIElementView<T>(T elementViewPrefab, Vector3 position) where T : UIView
        {
            var view = CreateUIElementView(elementViewPrefab);
            view.transform.position = position;

            return view;
        }

        public void DestroyUIElementView(UIView elementView)
        {
            elementView.Close();

            openedUIElements.Remove(elementView);

            if (elementView)
            {
                Destroy(elementView.gameObject);
            }
        }

        private void CloseOtherPopups()
        {
            if (openedUIElements != null)
            {
                for (var i = openedUIElements.Count - 1; i >= 0; i--)
                {
                    if (openedUIElements[i] is ISinglePopupView)
                    {
                        DestroyUIElementView(openedUIElements[i]);
                    }
                }
            }
        }
    }
}