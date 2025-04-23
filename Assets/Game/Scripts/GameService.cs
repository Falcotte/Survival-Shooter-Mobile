using SurvivalShooter.Services;
using UnityEngine.Events;

namespace SurvivalShooter.Game
{
    public class GameService : BaseService<IGameService>, IGameService
    {
        private GameState _currentState = GameState.MainMenu;
        public GameState CurrentState => _currentState;

        public UnityAction OnGameStart { get; set; }
        public UnityAction OnGameLose { get; set; }
        public UnityAction<GameState> OnGameStateChange { get; set; }

        private void ChangeState(GameState gameState)
        {
            _currentState = gameState;

            OnGameStateChange?.Invoke(gameState);
        }

        public void GoToMainMenuState()
        {
            ChangeState(GameState.MainMenu);
        }
        
        public void StartGame()
        {
            OnGameStart?.Invoke();
            
            ChangeState(GameState.Gameplay);
        }

        public void LoseGame()
        {
            OnGameLose?.Invoke();
            
            ChangeState(GameState.GameLose);
        }
    }
}