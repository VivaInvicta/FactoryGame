using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FactoryGame.Services
{
    public class ServiceLocator : MonoBehaviour
    {
        public event Action ServicesInitialized;

        private List<Service> services = new List<Service>();
        private bool isInitialized;

        public void AddServices(IEnumerable<Service> services)
        {
            foreach (var service in services)
            {
                service.Initialize(this);
                this.services.Add(service);
            }

            ServicesInitialized?.Invoke();
            isInitialized = true;
        }

        public T GetService<T>() where T : Service
        {
            return (T)services.FirstOrDefault(service => service is T);
        }

        public void Process(float deltaTime)
        {
            if (isInitialized)
            {
                foreach (var service in services)
                {
                    service.Process(Time.deltaTime);
                }
            }
        }
    }
}