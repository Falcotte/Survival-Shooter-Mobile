using System;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivalShooter.Services
{
    public static class ServiceLocator
    {
        private static Dictionary<Type, IService> _services = new();

        public static void RegisterService<T>(IService service) where T : IService
        {
            if(_services.ContainsKey(typeof(T)))
            {
                Debug.LogWarning($"{service.GetType().Name} is already registered as {typeof(T).Name}");
                return;
            }

            _services.Add(typeof(T), service);

            Debug.Log($"{service.GetType().Name} registered as {typeof(T).Name}");
        }

        public static void DeregisterService<T>(IService service) where T : IService
        {
            if(_services.ContainsKey(typeof(T)))
            {
                _services.Remove(typeof(T));

                Debug.Log($"{service.GetType().Name} deregistered");
            }
        }

        public static T GetService<T>() where T : IService
        {
            IService service = default;

            if(_services.TryGetValue(typeof(T), out service))
            {
                return (T)service;
            }

            return (T)service;
        }
    }
}