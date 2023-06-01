using FactoryGame.Configuration;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace FactoryGame.UI
{
    [RequireComponent(typeof(Button))]
    public abstract class QueueItemSelectorViewBase : UIView
    {
        public abstract event Action NextItemSelectPressed;

        public abstract ResourceDisplayerView ResourceDisplayerView { get; }
    }
}