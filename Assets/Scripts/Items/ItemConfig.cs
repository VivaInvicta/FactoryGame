using System;
using UnityEngine;

namespace FactoryGame.Configuration
{
    [Serializable]
    [CreateAssetMenu]
    public class ItemConfig : ScriptableObject
    {
        [SerializeField]
        private Sprite icon;

        [SerializeField]
        private string id;

        [SerializeField]
        private bool showInStorage = true;

        public Sprite Icon => icon;

        public string Id => id;
        public bool ShowInStorage => showInStorage;
    }
}