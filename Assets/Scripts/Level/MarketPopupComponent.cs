using FactoryGame.Configuration;
using FactoryGame.UI;
using UnityEngine;

namespace FactoryGame.Gameplay
{
    public class MarketPopupComponent : PopupBuildingComponent<MarketPopupModel, MarketPopupView, MarketPopupController>
    {
        [SerializeField]
        private MarketConfig marketConfig;

        protected override MarketPopupController CreateController(MarketPopupModel model, MarketPopupView view)
        {
            return new MarketPopupController(model, view);
        }

        protected override MarketPopupModel CreateModel()
        {
            return new MarketPopupModel(services, marketConfig);
        }
    }
}