using SurvivalShooter.Services;

namespace SurvivalShooter.Loading
{
    public interface ILoadingService : IService
    {
        public void LoadPermanentScenes();
    }
}