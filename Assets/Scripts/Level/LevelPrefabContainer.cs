using FactoryGame.Gameplay;
using UnityEngine;

namespace FactoryGame
{
    [CreateAssetMenu]
    public class LevelPrefabContainer : ScriptableObject
    {
        private Level levelPrefab;

        public Level LevelPrefab => levelPrefab;

        public void SetLevelPrefab(Level prefab)
        {
            levelPrefab = prefab;
        }
    }
}