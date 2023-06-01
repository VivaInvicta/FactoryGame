using UnityEngine;

namespace FactoryGame
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private LevelPrefabContainer levelContainer;

        private void OnEnable()
        {
            Instantiate(levelContainer.LevelPrefab);
        }
    }
}