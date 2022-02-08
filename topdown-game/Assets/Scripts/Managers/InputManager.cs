using topdownGame.Events;
using topdownGame.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace topdownGame.Managers {

    public class InputManager : MonoBehaviour {

        private PlayerControl m_playerController;

        private void OnEnable() {
            m_playerController.Enable();

            m_playerController.Player.Move.performed += MoveAction;
            m_playerController.Player.Move.canceled += MoveAction;
            m_playerController.Player.Dash.performed += DashAction;
            m_playerController.Player.Fire.performed += FireAction;
        }
        
        private void OnDisable() {
            m_playerController.Disable();
            
            m_playerController.Player.Move.performed -= MoveAction;
            m_playerController.Player.Move.canceled -= MoveAction;
            m_playerController.Player.Dash.performed -= DashAction;
        }

        private void Awake() {
            m_playerController = new PlayerControl();
        }

        private void MoveAction(InputAction.CallbackContext ctx) {
            GameManager.Instance.GlobalDispatcher.Emit(new OnMove(ctx.ReadValue<Vector2>()));
        }

        private void DashAction(InputAction.CallbackContext ctx) {
            GameManager.Instance.GlobalDispatcher.Emit(new OnDash());
        }

        private void FireAction(InputAction.CallbackContext ctx) {
            GameManager.Instance.GlobalDispatcher.Emit(new OnFire());
        }
    }
}