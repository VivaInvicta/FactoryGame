using UnityEngine;

namespace FactoryGame.Configuration
{
    [CreateAssetMenu]
    public class ItemsCollection : ScriptableObject
    {
        [SerializeField]
        private ItemConfig[] items;

        public ItemConfig[] Items => items;
    }
}