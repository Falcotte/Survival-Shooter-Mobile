using SurvivalShooter.Services;
using UnityEngine.Events;

namespace SurvivalShooter.Game
{
    public interface IGameService : IService
    {
        public UnityAction OnGameStart { get; set; }
        public UnityAction OnGameLose { get; set; }
        public UnityAction OnGameReset { get; set; }
        public UnityAction<GameState> OnGameStateChange { get; set; }

        public void GoToMainMenuState();
        
        public void StartGame();

        public void LoseGame();
        
        public void ResetGame();
    }
}