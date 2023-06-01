using FactoryGame.Services.GameData;
using UnityEngine;

namespace FactoryGame.Services
{
    [CreateAssetMenu(fileName = nameof(AutoSaveService), menuName = "Services/" + nameof(AutoSaveService))]
    public class AutoSaveService : Service
    {
        [Tooltip("In seconds")]
        [SerializeField]
        private float autoSavePeriodicity;

        private GameDataService gameDataService;
        private float timePassed;

        protected override void InitializeInternal()
        {
            services.ServicesInitialized += OnServicesInitialized;
        }

        protected override void ProcessInternal(float deltaTime)
        {
            timePassed += deltaTime;

            if (timePassed > autoSavePeriodicity)
            {
                timePassed = 0;
                Save();
            }
        }

        private void OnServicesInitialized()
        {
            gameDataService = services.GetService<GameDataService>();
        }

        private void Save()
        {
            gameDataService.Save();
        }
    }
}