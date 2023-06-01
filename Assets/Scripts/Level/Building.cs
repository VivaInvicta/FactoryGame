using FactoryGame.Services;
using System.Collections.Generic;
using UnityEngine;

namespace FactoryGame.Gameplay
{
    public class Building : MonoBehaviour
    {
        private IEnumerable<BuildingComponent> components;

        private bool IsInitialized;

        public void Initialize(ServiceLocator services)
        {
            components = GetComponents<BuildingComponent>();

            foreach (var component in components)
            {
                component.Initialize(services);
            }

            IsInitialized = true;
        }

        public void Process(float deltaTime)
        {
            if (!IsInitialized)
                return;

            foreach (var component in components)
            {
                component.Process(deltaTime);
            }
        }
    }
}
