using System.Collections;
using System.Collections.Generic;
using topdownGame.Actions;
using topdownGame.Managers;
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
        public OnFire(bool firing) {
            Firing = firing;
        }

        public bool Firing;
    }

    public class OnBulletHit {
        public OnBulletHit(EmitterInfo emitter, ReceiverInfo receiver) {
            Emitter = emitter;
            Receiver = receiver;
        }
        
        public struct EmitterInfo {
            public int Damage;
            public GameObject Object;
            public Character Character;
        }
        
        public struct ReceiverInfo {
            public GameObject Object;
            public Character Character;
        }
        
        public EmitterInfo Emitter;
        public ReceiverInfo Receiver;
    }

    public class OnLifeUpdate {
        public OnLifeUpdate(Character character, int amount) {
            Character = character;
            Amount = amount;
        }

        public int Amount;
        public Character Character;
    }

    public class OnPickUpItem {
        public OnPickUpItem() { }
    }
    
    public class OnPickedWeapon {
        public OnPickedWeapon(GameObject weapon) {
            Weapon = weapon;
        }

        public GameObject Weapon;
    }

    public class OnDamageText {
        public OnDamageText(Vector3 position, int damage) {
            Position = position;
            Damage = damage;
        }

        public Vector3 Position;
        public int Damage;
    }
}

