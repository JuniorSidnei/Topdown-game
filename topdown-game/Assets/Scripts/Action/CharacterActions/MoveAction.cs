using System.Collections;
using System.Collections.Generic;
using topdownGame.Actions;
using topdownGame.Events;
using topdownGame.Managers;
using topdownGame.Player;
using topdownGame.Utils;
using UnityEngine;

namespace topdownGame.Actions {
    
    [System.Serializable]
    public class MoveAction : Action  {
        
        public float speed;
        private Vector2 m_inputDirection;
        private Character m_char;
        
        public override void OnConfigure() {
            m_char = Character;
        }

        public override void OnActivate() {
            m_char.CharacterDispatcher.Subscribe<OnMove>(OnMove);
            m_char.CharacterDispatcher.Subscribe<OnCharacterFixedUpdate>(OnCharacterFixedUpdate);
        }

        public override void OnDeactivate() {
            m_char.CharacterDispatcher.Unsubscribe<OnMove>(OnMove);
        }

        private void OnMove(OnMove onMove) {
            m_inputDirection = onMove.InputDirection;
        }

        private void OnCharacterFixedUpdate(OnCharacterFixedUpdate ev) {
            m_char.Velocity = m_inputDirection * speed;
        }
    }
}
