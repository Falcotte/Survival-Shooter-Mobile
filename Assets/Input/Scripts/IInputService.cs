using SurvivalShooter.Services;

namespace SurvivalShooter.Inputs
{
    public interface IInputService : IService
    {
        public void RegisterController(IInputController controller);

        public void DeregisterController(IInputController controller);

        public IInputController GetController(InputControllerType controllerType);
    }
}