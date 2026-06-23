using System;
using UnityEngine.InputSystem;

namespace Minesweeper.Runtime.Infrastructure
{
    public class InputsService : IInputsService, IDisposable
    {
        private readonly InputsMap _inputsMap;

        public event Action OnRestartPressed;
        public event Action OnPausePressed;

        public InputsService()
        {
            _inputsMap = new InputsMap();
            _inputsMap.Game.Enable();
            
            Subscribe();
        }

        private void Subscribe()
        {
            _inputsMap.Game.Restart.performed += HandleRestartPressed;
            _inputsMap.Game.Pause.performed += HandlePausePressed;
        }

        private void Unsubscribe()
        {
            _inputsMap.Game.Restart.performed -= HandleRestartPressed;
            _inputsMap.Game.Pause.performed -= HandlePausePressed;
        }

        private void HandleRestartPressed(InputAction.CallbackContext ctx) => OnRestartPressed?.Invoke();
        private void HandlePausePressed(InputAction.CallbackContext obj) => OnPausePressed?.Invoke();

        public void Dispose() => Unsubscribe();
    }
}