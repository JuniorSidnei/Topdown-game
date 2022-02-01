using System.Collections;
using System.Collections.Generic;
using topdownGame.Controller;
using UnityEngine;

namespace topdownGame.Actions {
    [RequireComponent(typeof(Controller2D))]
    public class Character : MonoBehaviour {

        [SerializeField] private float m_drag;
        
        private Controller2D m_controller;

        private Vector3 m_velocity;
        private Vector3 m_positionDelta;

        public Vector3 Velocity {
            get => m_velocity;
            set => m_velocity = value;
        }
        
        private void Start() {
            m_controller = GetComponent<Controller2D>();
        }


        private void FixedUpdate() {
            var oldPos = transform.position;
            m_velocity *= (1 - Time.deltaTime * m_drag);
            m_controller.Move(m_velocity * Time.deltaTime);
            m_positionDelta = transform.position - oldPos;
        }
    }
}