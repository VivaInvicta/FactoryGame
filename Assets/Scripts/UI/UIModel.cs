using FactoryGame.Services;

namespace FactoryGame.UI
{
    public abstract class UIModel
    {
        public ServiceLocator Services => services;

        protected ServiceLocator services;

        public UIModel(ServiceLocator services)
        {
            this.services = services;
        }

        public void Release()
        {
            services = null;

            ReleaseInternal();
        }

        protected virtual void ReleaseInternal() { }
    }
}