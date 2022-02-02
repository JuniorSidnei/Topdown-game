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
        [SerializeField] private float m_acceleration;

        private Character m_character;
        
        private Vector2 m_inputDirection;
        private Vector3 m_velocitySmoothing;


        private void Awake() {
            GameManager.Instance.GlobalDispatcher.Subscribe<OnMove>(OnMove);
            m_character = GetComponent<Character>();
        }

        private void OnMove(OnMove onMove) {
            m_inputDirection = onMove.InputDirection;
        }

        private void FixedUpdate() {
            var targetVelocity = m_inputDirection * m_speed;
            //var velocity = m_character.Velocity;
            //velocity.x += Mathf.SmoothDamp(m_character.Velocity.x, targetVelocity.x, ref m_velocitySmoothing, m_acceleration);
            //velocity.y += Mathf.SmoothDamp(m_character.Velocity.y, targetVelocity.y, ref m_velocitySmoothing, m_acceleration);
            m_character.Velocity = Vector3.SmoothDamp(m_character.Velocity, targetVelocity, ref m_velocitySmoothing,
                m_acceleration);
            //m_character.Velocity += new Vector3(m_inputDirection.x, m_inputDirection.y) * m_speed;
        }
    }
}
