using UnityEngine;

namespace FactoryGame.Services
{
    public abstract class Service : ScriptableObject
    {
        protected ServiceLocator services;

        public void Initialize(ServiceLocator services)
        {
            this.services = services;

            InitializeInternal();
        }

        public void Process(float deltaTime) => ProcessInternal(deltaTime);

        public void Release() => ReleaseInternal();

        protected virtual void InitializeInternal()
        { }

        protected virtual void ProcessInternal(float deltaTime)
        { }

        protected virtual void ReleaseInternal()
        { }
    }
}