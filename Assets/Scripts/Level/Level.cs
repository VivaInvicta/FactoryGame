using FactoryGame.Services;
using FactoryGame.UI;
using UnityEngine;

namespace FactoryGame.Gameplay
{
    public class Level : MonoBehaviour
    {
        [SerializeField]
        private Building[] buildings;

        [SerializeField]
        private Service[] services;

        [SerializeField]
        private LevelGUI levelGUI;

        private ServiceLocator serviceLocator;

        private void OnEnable()
        {
            AddServices();

            foreach (var building in buildings)
            {
                building.Initialize(serviceLocator);
            }

            levelGUI.Initialize(serviceLocator);
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;

            foreach (var building in buildings)
            {
                building.Process(deltaTime);
            }

            serviceLocator.Process(deltaTime);
        }

        private void AddServices()
        {
            serviceLocator = new ServiceLocator();
            serviceLocator.AddServices(services);
        }
    }
}