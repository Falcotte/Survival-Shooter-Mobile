using SurvivalShooter.Services;
using UnityEngine;

namespace SurvivalShooter.UI
{
    public interface IUIService : IService
    {
        public Camera UICamera { get; }
        public Canvas Canvas { get; }
    }
}