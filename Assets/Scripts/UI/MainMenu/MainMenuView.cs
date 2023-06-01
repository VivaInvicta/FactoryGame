using FactoryGame.Gameplay;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FactoryGame.UI
{
    public class MainMenuView : UIView
    {
        public event Action StartButtonPressed;
        public event Action<Level> LevelSelected;

         [SerializeField]
        private ButtonLevelPair[] levelButtons;

        [SerializeField]
        private Button startButton;

        public void SetStartButtonInteractable(bool isInteractable)
        {
            startButton.interactable = isInteractable;
        }

        public void SetLevelButtonSelected(Level attachedLevel)
        {
            foreach (var button in levelButtons)
            {
                button.Button.interactable = !(button.Prefab == attachedLevel);   
            }
        }

        private void OnEnable()
        {
            foreach (var levelButton in levelButtons)
            {
                levelButton.Button.onClick.AddListener(() => LevelSelected?.Invoke(levelButton.Prefab));
            }
            startButton.onClick.AddListener(() => StartButtonPressed?.Invoke());
        }

        private void OnDisable()
        {
            foreach (var levelButton in levelButtons)
            {
                levelButton.Button.onClick.RemoveAllListeners();
            }

            startButton.onClick.RemoveAllListeners();
        }

        [Serializable]
        private class ButtonLevelPair
        {
            [SerializeField]
            private Level prefab;

            [SerializeField]
            private Button button;

            public Level Prefab => prefab;
            public Button Button => button;
        }

    }

   
}