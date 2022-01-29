using System.Collections;
using System.Collections.Generic;
using topdownGame.Controller;
using UnityEngine;

namespace topdownGame.Player {

    [RequireComponent(typeof(Controller2D))]
    public class Character : MonoBehaviour {

        private Controller2D m_controller;
        private Vector3 m_velocity;
        
        private void Awake() {
            m_controller = GetComponent<Controller2D>();
        }

       
        private void FixedUpdate() {
            m_controller.Move(m_velocity);
        }
    }
}