using SurvivalShooter.Services;

namespace SurvivalShooter.Audio
{
    public interface IAudioService : IService
    {
        public void PlayAudio(AudioKey audioKey);
    }
}