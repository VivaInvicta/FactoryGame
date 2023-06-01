using FactoryGame.UI;
using UnityEngine;

namespace FactoryGame
{
    public class MainMenuHandler : MonoBehaviour
    {
        [SerializeField]
        private string levelSceneName;

        [SerializeField]
        private LevelPrefabContainer levelContainer;

        [SerializeField]
        private MainMenuView view;

        private void OnEnable()
        {
            CreateController();
        }
      
        private void CreateController()
        {
            var model = new MainMenuModel(null, levelContainer, levelSceneName);
            var controller = new MainMenuController(model, view);
        }
    }
}