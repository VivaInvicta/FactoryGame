using FactoryGame.Configuration;
using FactoryGame.Services;
using FactoryGame.Services.GameData;
using System;
using System.Collections.Generic;

namespace FactoryGame.UI
{
    public class QueueItemSelectorModel : UIModel
    {
        public event Action Updated;

        private GameDataService gameDataService;

        private Queue<string> itemIdsQueue;

        private ResourceDisplayerModel resourceDisplayer;

        public (ItemConfig config, int count) SelectedItem => resourceDisplayer.DisplayedResource;

        public QueueItemSelectorModel(ServiceLocator services, IEnumerable<string> selectableItems)
            : base(services)
        {
            itemIdsQueue = new Queue<string>(selectableItems);

            gameDataService = services.GetService<GameDataService>();
        }

        public void AddResourceDisplayerModel(ResourceDisplayerModel model)
        {
            resourceDisplayer = model;

            resourceDisplayer.DisplayedResourceUpdated += ResourceDisplayerUpdated;
        }

        public void SkipToItem(string itemId)
        {
            var nextItemId = SelectedItem.config.Id;

            if (!itemIdsQueue.Contains(itemId))
                return;

            while (nextItemId != itemId)
            {
                nextItemId = itemIdsQueue.Dequeue();
                itemIdsQueue.Enqueue(nextItemId);
            }

            SetItemSelected(nextItemId);
        }

        public void SelectNextItem()
        {
            var nextItemId = itemIdsQueue.Dequeue();

            SetItemSelected(nextItemId);

            itemIdsQueue.Enqueue(nextItemId);
        }

        protected override void ReleaseInternal()
        {
            gameDataService = null;

            resourceDisplayer.DisplayedResourceUpdated -= ResourceDisplayerUpdated;
            resourceDisplayer.Release();
            resourceDisplayer = null;

            itemIdsQueue.Clear();
        }

        private void ResourceDisplayerUpdated()
        {
            Updated?.Invoke();
        }

        private void SetItemSelected(string itemId)
        {
            resourceDisplayer.SetDisplayedResource(itemId);
        }
    }
}