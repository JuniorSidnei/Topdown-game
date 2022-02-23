using System.Collections;
using System.Collections.Generic;
using topdownGame.Controller;
using topdownGame.Events;
using topdownGame.Managers;
using topdownGame.Utils;
using UnityEngine;

namespace topdownGame.Actions {
    
    [RequireComponent(typeof(Controller2D))]
    public class Character : MonoBehaviour {

        [SerializeField] private float m_drag;
        
        private Controller2D m_controller;
        private Collision2DProxy m_collisionProxy;

        private Vector3 m_velocity;
        private Vector3 m_positionDelta;

        public Vector3 Velocity {
            get => m_velocity;
            set => m_velocity = value;
        }

        public Vector3 PositionDelta {
            get => m_positionDelta;
        }
        
        public Collision2DProxy CollisionProxy => m_collisionProxy;
        
        public delegate void InteractAction(Character character);
        public static event InteractAction OnInteract;
        
        private void Awake() {
            m_controller = GetComponent<Controller2D>();
            m_collisionProxy = GetComponent<Collision2DProxy>();
            
            GameManager.Instance.GlobalDispatcher.Subscribe<OnPickUpItem>(OnPickUpItem);
        }
        
        private void FixedUpdate() {
            var oldPos = transform.position;
            m_velocity *= (1 - Time.deltaTime * m_drag);
            m_controller.Move(m_velocity * Time.deltaTime);
            m_positionDelta = transform.position - oldPos;
        }

        private void OnPickUpItem(OnPickUpItem ev) {
            OnInteract?.Invoke(this);
        }
    }
}