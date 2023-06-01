using System;
using System.Collections.Generic;
using UnityEngine;

namespace FactoryGame.Configuration
{
    [CreateAssetMenu]
    [Serializable]
    public class ItemRecipes : ScriptableObject
    {
        [SerializeField]
        private ItemRecipe[] recipes;

        public ItemRecipe[] Recipes => recipes;
    }

    [Serializable]
    public class ItemRecipe
    {
        [SerializeField]
        string[] materials;

        [SerializeField]
        string result;

        public IEnumerable<string> Materials => materials;
        public string Result => result;
    }
}
