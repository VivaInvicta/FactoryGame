using FactoryGame.Services.GameData;
using FactoryGame.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FactoryGame.Services
{
    [CreateAssetMenu(fileName = nameof(GameWinService), menuName = "Services/" + nameof(GameWinService))]
    public class GameWinService : Service
    {
        [SerializeField]
        private string targetResourceId;

        [SerializeField]
        private int targetResourceCount;

        [SerializeField]
        private string menuSceneName;

        [SerializeField]
        private SimplePopupWithButtonView popupPrefab;

        private ResourcesData resources;
        private UIService uiService;
        private GameDataService gameDataService;

        protected override void InitializeInternal()
        {
            services.ServicesInitialized += OnServicesInitialized;
        }

        protected override void ReleaseInternal()
        {
            resources.OnUpdate -= OnResourceUpdated;
            resources = null;
        }

        private void OnServicesInitialized()
        {
            gameDataService = services.GetService<GameDataService>();
            resources = gameDataService.Resources;
            resources.OnUpdate += OnResourceUpdated;

            uiService = services.GetService<UIService>();

            CheckWin();
        }

        private void CheckWin()
        {
            var targetResourceCurrentCount = resources.GetResourceCountById(targetResourceId);
            if (targetResourceCurrentCount >= targetResourceCount)
                WinGame();
        }

        private void OnResourceUpdated(string resource, int newCount)
        {
            if (resource == targetResourceId && newCount >= targetResourceCount)
                WinGame();
        }

        private void WinGame()
        {
            var winPopup = uiService.CreateUIElementView(popupPrefab);
            winPopup.ButtonClicked += OnPopupButtonClicked;
        }

        private void OnPopupButtonClicked()
        {
            gameDataService.ClearSave();
            SceneManager.LoadScene(menuSceneName);
        }
    }
}