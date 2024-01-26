using UnityEngine;

namespace SurvivalShooter.Services
{
    public abstract class BaseService<T> : MonoBehaviour, IService where T : IService
    {
        protected virtual void Awake()
        {
            RegisterService();
        }

        public void RegisterService()
        {
            ServiceLocator.RegisterService<T>(this);
        }
    }
}