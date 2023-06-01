using UnityEngine;
using UnityEngine.UI;

namespace FactoryGame.UI
{
    public class RecipeDisplayerView : UIView
    {
        [SerializeField]
        private QueueItemSelectorViewBase[] materialSelectors;

        [SerializeField]
        private Image resultImage;

        [SerializeField]
        private Text resultCount;

        [SerializeField]
        private Transform countPlaceholder;

        [SerializeField]
        private ResourceDisplayerView resultDisplayer;

        public QueueItemSelectorViewBase[] MaterialSelectors => materialSelectors;
        public ResourceDisplayerView ResultDisplayerView => resultDisplayer;

        public void SetResultEnabled(bool isEnabled)
        {
            resultDisplayer.gameObject.SetActive(isEnabled);
        }

        protected override void SetInteractableInternal(bool interactable)
        {
            foreach (var selector in materialSelectors)
            {
                selector.SetInteractable(interactable);
            }
        }
    }
}