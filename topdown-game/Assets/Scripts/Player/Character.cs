using System;
using System.Collections;
using System.Collections.Generic;
using topdownGame.Actions;
using topdownGame.Controller;
using topdownGame.Events;
using topdownGame.Managers;
using topdownGame.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace topdownGame.Player {

    [RequireComponent(typeof(Controller2D))]
    public class Character : MonoBehaviour {
        
        //move this to dash action
        public float timeDashApex;
        //move this to dash action
        public float dashImpulse;
        
        public readonly QueuedEventDispatcher CharacterDispatcher = new();
        [SerializeReference]
        public List<IAction> CharacterActions = new();

        private Controller2D m_controller;
        
        //move this to dash action
        private float m_timeDashApexReset;
        private float m_drag;
        private Vector3 m_velocity;
        
        private Vector3 m_positionDelta;
        //move this to dash action
        private Vector2 m_dashVelocity = Vector2.one;

        #region properties
        public Vector3 Velocity {
            get => m_velocity;
            set => m_velocity = value;
        }
        
        public Vector3 PositionDelta {
            get => m_positionDelta;
            set => m_positionDelta = value;
        }

        public float Drag => m_drag;

        #endregion
        
        private void Awake() {
            m_controller = GetComponent<Controller2D>();
            
            foreach (var action in CharacterActions) {
                action.Configure(this);
            }
            
            //move this to action dash
            m_timeDashApexReset = timeDashApex;
        }


        // private void OnDash(GlobalEvents.OnDash onDash) {
        //     var dashForce = (2 * dashImpulse) / Mathf.Pow(timeDashApex, 2);
        //     m_dashVelocity = new Vector2(dashForce, dashForce);
        // }

        private void Update() {
            CharacterDispatcher.DispatchAll();
        }

        private void FixedUpdate() {
            CharacterDispatcher.EmitImmediate(new OnCharacterFixedUpdate());
            
            var oldPos = transform.position;
            m_velocity *= (1 - Time.deltaTime * m_drag);
            m_controller.Move(m_velocity * Time.deltaTime);
            PositionDelta = transform.position - oldPos;
        }
    }
}