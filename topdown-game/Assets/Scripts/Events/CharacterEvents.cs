using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace topdownGame.Events {
    
    public class OnMove {
        public OnMove(Vector2 inputDirection) {
            InputDirection = inputDirection;
        }

        public Vector2 InputDirection;
    }

    public class OnDash {
        public OnDash() { }
    }

    public class OnStaminaUpdate {
        public OnStaminaUpdate(int amount) {
            Amount = amount;
        }

        public int Amount;
    }

    public class OnFire {
        public OnFire() { }
    }
}

