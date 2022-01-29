using System.Collections;
using System.Collections.Generic;
using topdownGame.Controller;
using UnityEngine;

namespace topdownGame.Player {

    [RequireComponent(typeof(Controller2D))]
    public class Player : MonoBehaviour {

        private Controller2D m_controller;
        
        private void Awake() {
            m_controller = GetComponent<Controller2D>();
        }

       
        private void Update() {

        }
    }
}