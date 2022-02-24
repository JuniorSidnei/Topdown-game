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

    public class OnSimpleBulletHit {
        public OnSimpleBulletHit(OnHitEmitterInfo emitter, OnHitReceiverInfo receiver) {
            Emitter = emitter;
            Receiver = receiver;
        }
        
        public struct OnHitEmitterInfo {
            public int EmitterDamage;
            public GameObject EmitterObject;
            public Character Character;
        }
        
        public struct OnHitReceiverInfo {
            public GameObject ReceiverObject;
            public Character Character;
        }
        
        public OnHitEmitterInfo Emitter;
        public OnHitReceiverInfo Receiver;
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

