using FactoryGame.Gameplay;

namespace FactoryGame.UI
{
    public class MainMenuController : UIController<MainMenuModel, MainMenuView>
    {
        public MainMenuController(MainMenuModel model, MainMenuView view) 
            : base(model, view)
        {
            view.LevelSelected += OnLevelSelected;
            view.StartButtonPressed += OnStartPressed;
        }

        private void OnLevelSelected(Level level)
        {
            model.SetSelectedLevel(level);
            view.SetStartButtonInteractable(true);
            view.SetLevelButtonSelected(level);
        }

        private void OnStartPressed()
        {
            model.StartGame();
        }
    }
}