using System;
using System.Collections;
using System.Collections.Generic;
using topdownGame.Actions;
using topdownGame.Controller;
using topdownGame.Events;
using topdownGame.Managers;
using topdownGame.Utils;
using UnityEngine;

namespace topdownGame.Actions {
    
    [RequireComponent(typeof(Controller2D))]
    public class MoveAction : MonoBehaviour  {
        
        [SerializeField] private float m_speed;

        private Character m_character;
        
        private Vector2 m_inputDirection;
        
        private void Awake() {
            GameManager.Instance.GlobalDispatcher.Subscribe<OnMove>(OnMove);
            m_character = GetComponent<Character>();
        }

        private void OnMove(OnMove onMove) {
            m_inputDirection = onMove.InputDirection;
        }

        private void FixedUpdate() {
            m_character.Velocity += new Vector3(m_inputDirection.x, m_inputDirection.y) * m_speed;
        }
    }
}
