using System;
using System.Collections.Generic;

namespace FactoryGame.Services.GameData
{
    [Serializable]
    public class ResourcesData
    {
        public event Action<string, int> OnUpdate;

        private Dictionary<string, int> resourcesDictionary = new Dictionary<string, int>();

        internal Dictionary<string, int> ResourcesDictionary { get => resourcesDictionary; set => resourcesDictionary = value; }

        public int GetResourceCountById(string id)
        {
            if (ResourcesDictionary.TryGetValue(id, out var count))
            {
                return count;
            }
            else
            {
                return 0;
            }
        }

        public void AddResource(string name, int count)
        {
            if (ResourcesDictionary.ContainsKey(name))
            {
                ResourcesDictionary[name] += count;
            }
            else
            {
                ResourcesDictionary.Add(name, count);
            }

            OnUpdate?.Invoke(name, ResourcesDictionary[name]);
        }

        public bool TryConsumeResource(string name, int count)
        {
            if (ResourcesDictionary.TryGetValue(name, out var currentCount))
            {
                if (currentCount > count)
                {
                    ResourcesDictionary[name] -= count;
                    OnUpdate?.Invoke(name, ResourcesDictionary[name]);

                    return true;
                }
            }

            return false;
        }

        public void ConsumeResource(string name, int count)
        {
            if (ResourcesDictionary.ContainsKey(name))
            {
                if (ResourcesDictionary[name] > count)
                {
                    ResourcesDictionary[name] -= count;
                    OnUpdate?.Invoke(name, ResourcesDictionary[name]);
                }
                else
                {
                    ResourcesDictionary.Remove(name);
                    OnUpdate?.Invoke(name, 0);
                }
            }
        }

    }
}