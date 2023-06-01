using FactoryGame.Gameplay;
using FactoryGame.Services;
using UnityEngine.SceneManagement;

namespace FactoryGame.UI
{
    public class MainMenuModel : UIModel
    {
        private Level selectedLevel;
        private LevelPrefabContainer levelContainer;
        private string gameSceneName;

        public MainMenuModel(ServiceLocator services, LevelPrefabContainer levelContainer, string gameSceneName) 
            : base(services)
        {
            this.levelContainer = levelContainer;
            this.gameSceneName = gameSceneName;
        }

        public void SetSelectedLevel(Level level)
        {
            selectedLevel = level;
        }

        public void StartGame()
        {
            levelContainer.SetLevelPrefab(selectedLevel);
            SceneManager.LoadScene(gameSceneName);
        }
    }
}
