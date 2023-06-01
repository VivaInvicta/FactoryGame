using FactoryGame.Services;
using UnityEngine;

namespace FactoryGame.UI
{
    public class LevelGUI : MonoBehaviour
    {
        [SerializeField]
        private StorageView guiStorageViewPrefab;

        [SerializeField]
        private ResourceDisplayerView resourceDisplayer;

        [SerializeField]
        private string displayedResourceId;

        private ServiceLocator services;
        private UIService uiService;

        public void Initialize(ServiceLocator services)
        {
            this.services = services;
            uiService = services.GetService<UIService>();

            CreateStorageController();
            CreateResourceDisplayerController();
        }

        private void CreateStorageController()
        {
            var storageModel = new StorageModel(services);
            var storageView = uiService.CreateUIElementView(guiStorageViewPrefab);

            var storageController = new StorageControler(storageModel, storageView);
        }

        private void CreateResourceDisplayerController()
        {
            var displayerModel = new ResourceDisplayerModel(services);
            var displayerView = uiService.CreateUIElementView(resourceDisplayer);

            var displayerController = new ResourceDisplayerController(displayerModel, displayerView);

            displayerModel.SetDisplayedResource(displayedResourceId);
        }
    }
}