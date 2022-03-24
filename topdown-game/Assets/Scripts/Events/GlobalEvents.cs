using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace topdownGame.Events {

    public class OnCameraScreenShake {

        public OnCameraScreenShake(float force, float duration) {
            Force = force;
            Duration = duration;
        }

        public float Force;
        public float Duration;
    }
    
    public class OnEnemySeekerDeath {
        public OnEnemySeekerDeath(GameObject seekerObject) {
            SeekerObject = seekerObject;
        }

        public GameObject SeekerObject;
    }
}