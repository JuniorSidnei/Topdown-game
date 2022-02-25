using System;
using DG.Tweening;
using topdownGame.Events;
using topdownGame.Managers;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace topdownGame.Actions {

    public class AimAction : MonoBehaviour {

        public new Camera camera;
        public SpriteRenderer AimRend;
        public SpriteRenderer TargetRend;
        public bool IsWeapon;
        private float m_aimAngle;
        private Vector3 m_mouseWorldPosition;
        
        private Character m_character;
        
        public float AimAngle => m_aimAngle;

        //public Vector3 MouseWorldPosition => m_mouseWorldPosition.normalized;
        public Vector2 AimDirection => m_mouseWorldPosition -  transform.position;
        
        private void Awake() {
            m_character = GetComponent<Character>();
        }

        private void Update() {
            if (camera != null) {
                Vector3 mousePos = Mouse.current.position.ReadValue();
                mousePos.z = -camera.transform.position.z;
                m_mouseWorldPosition = camera.ScreenToWorldPoint(mousePos);
            }

            if (!IsWeapon) return;

            m_aimAngle = AngleBetweenPoints(transform.position, m_mouseWorldPosition);
            
            TargetRend.flipY = m_aimAngle is < -90 or > 90;
            //TargetRend.sortingOrder = TargetRend.flipY ? 1 : 0;
       
            transform.rotation =  Quaternion.Euler (new Vector3(0f,0f,m_aimAngle));
            AimRend.gameObject.transform.position = m_mouseWorldPosition;
            AimRend.gameObject.transform.rotation = quaternion.Euler(new Vector3(0, 0,0));
        }
        
        private float AngleBetweenPoints(Vector2 a, Vector2 b) {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }

        public void Show(bool enable) {
            AimRend.gameObject.SetActive(enable);
            if (TargetRend.flipY) TargetRend.flipY = false;
            enabled = enable;
        }

        public bool IsWeaponFlipped() {
            return TargetRend.flipY;
        }
    }
}