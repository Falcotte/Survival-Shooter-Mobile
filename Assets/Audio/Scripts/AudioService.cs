using DG.Tweening;
using SurvivalShooter.Pooling;
using SurvivalShooter.Services;
using UnityEngine;

namespace SurvivalShooter.Audio
{
    public class AudioService : BaseService<IAudioService>, IAudioService
    {
        [SerializeField] private SerializableDictionary<AudioKey, AudioClip> _audioClips = new();

        [SerializeField] private SerializableDictionary<MusicKey, AudioClip> _musicClips = new();

        private IPoolService _poolService;
        private ObjectPool<PoolableAudioSource> _audioSourcePool;

        private void Start()
        {
            _poolService = ServiceLocator.Get<IPoolService>();
            _audioSourcePool = _poolService.AudioSourcePool;

            PlayMusic(MusicKey.Background);
        }

        public void PlayAudio(AudioKey audioKey)
        {
            if (_audioClips.TryGetValue(audioKey, out var clip))
            {
                PoolableAudioSource audioSource = _audioSourcePool.Get(PoolKey.AudioSource);

                Sequence audioSequence = DOTween.Sequence();
                audioSequence.AppendCallback(() =>
                {
                    audioSource.AudioSource.clip = clip;
                    audioSource.AudioSource.Play();
                });
                audioSequence.AppendInterval(clip.length);
                audioSequence.AppendCallback(() =>
                {
                    if (!audioSource) return;

                    _audioSourcePool.Return(audioSource);
                });
            }
            else
            {
                Debug.LogWarning($"Audio clip with key {audioKey} not found");
            }
        }

        public void PlayMusic(MusicKey musicKey)
        {
            if (_musicClips.TryGetValue(musicKey, out var clip))
            {
                PoolableAudioSource audioSource = _audioSourcePool.Get(PoolKey.AudioSource);

                audioSource.AudioSource.clip = clip;
                audioSource.AudioSource.loop = true;
                audioSource.AudioSource.Play();
            }
            else
            {
                Debug.LogWarning($"Music clip with key {musicKey} not found");
            }
        }
    }

    public enum AudioKey
    {
        PlayerGunShot,
        PlayerHurt,
        PlayerDeath,
        ZombunnyHurt,
        ZombunnyDeath,
        ZombearHurt,
        ZombearDeath,
        HellephantHurt,
        HellephantDeath,
    }

    public enum MusicKey
    {
        Background,
    }
}