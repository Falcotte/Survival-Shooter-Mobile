using SurvivalShooter.Services;
using UnityEngine.Events;

namespace SurvivalShooter.Game
{
    public class GameService : BaseService<IGameService>
    {
        private GameState _currentState = GameState.MainMenu;
        public GameState CurrentState => _currentState;

        public UnityAction OnGameStart;
        public UnityAction OnGameLose;

        public UnityAction<GameState> OnGameStateChange;

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