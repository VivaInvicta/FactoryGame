using UnityEngine;

namespace FactoryGame.UI
{
    public class StorageView : UIView
    {
        [SerializeField]
        private ResourceDisplayerView resourceDisplayerViewPrefab;

        [SerializeField]
        private Transform resourceDisplayersContainer;

        public ResourceDisplayerView ResourceDisplayerViewPrefab => resourceDisplayerViewPrefab;

        public void AddResourceDisplayer(ResourceDisplayerView view)
        {
            view.transform.SetParent(resourceDisplayersContainer);
        }
    }
}