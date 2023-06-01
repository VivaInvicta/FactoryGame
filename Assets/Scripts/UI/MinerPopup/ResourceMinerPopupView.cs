using System;
using UnityEngine;
using UnityEngine.UI;

namespace FactoryGame.UI
{
    public class ResourceMinerPopupView : UIView, ISinglePopupView
    {
        public event Action StartButtonPressed;
        public event Action StopButtonPressed;
        public event Action BackgroundPressed;

        [SerializeField]
        private QueueItemSelectorViewBase itemSelector;

        [SerializeField]
        private Button startButton;

        [SerializeField]
        private Button stopButton;

        [SerializeField]
        private Button backgroundButton;

        public QueueItemSelectorViewBase ItemSelectorView => itemSelector;

        public void ShowStartButton()
        {
            startButton.gameObject.SetActive(true);
            stopButton.gameObject.SetActive(false);
        }

        public void ShowStopButton()
        {
            startButton.gameObject.SetActive(false);
            stopButton.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            startButton.onClick.AddListener(OnStartButtonPressed);
            stopButton.onClick.AddListener(OnStopButtonPressed);
            backgroundButton.onClick.AddListener(OnBackgroundPressed);
        }

        private void OnDisable()
        {
            startButton.onClick.RemoveAllListeners();
            backgroundButton.onClick.RemoveAllListeners();
            stopButton.onClick.RemoveAllListeners();
        }

        private void OnStartButtonPressed()
        {
            StartButtonPressed?.Invoke();
        }

        private void OnStopButtonPressed()
        {
            StopButtonPressed?.Invoke();
        }

        private void OnBackgroundPressed()
        {
            BackgroundPressed?.Invoke();
        }
    }
}