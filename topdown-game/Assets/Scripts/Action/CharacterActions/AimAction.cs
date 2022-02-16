using System;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace topdownGame.Actions {

    public class AimAction : MonoBehaviour {

        public SpriteRenderer AimRend;
        public SpriteRenderer TargetRend;
        private float m_aimAngle;

        public float AimAngle => m_aimAngle;

        private Vector3 m_rightPosition;
        private Vector3 m_leftPosition;

        private void Update() {
            var mouseWorldPosition = Camera.main.ScreenToWorldPoint((Vector3)Mouse.current.position.ReadValue() + Vector3.forward * 10f);
            
            var angle = AngleBetweenPoints(transform.position, mouseWorldPosition);
            
            transform.rotation =  Quaternion.Euler (new Vector3(0f,0f,angle));
            
            TargetRend.flipY = angle is <= -90 or >= 90;
            
            AimRend.gameObject.transform.position = mouseWorldPosition;
            AimRend.gameObject.transform.rotation = quaternion.Euler(new Vector3(0, 0,0));
        }
        
        private float AngleBetweenPoints(Vector2 a, Vector2 b) {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }
    }
}