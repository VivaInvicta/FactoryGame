using FactoryGame.Services;
using UnityEngine;

namespace FactoryGame.Gameplay
{
    public abstract class BuildingComponent : MonoBehaviour
    {
        protected ServiceLocator services;

        public void Initialize(ServiceLocator services)
        {
            this.services = services;
            InitializeInternal();
        }

        public void Process(float deltaTime)
        {
            ProcessInternal(deltaTime);
        }

        protected virtual void InitializeInternal() { }
        protected virtual void ProcessInternal(float deltaTime) { }
    }
}