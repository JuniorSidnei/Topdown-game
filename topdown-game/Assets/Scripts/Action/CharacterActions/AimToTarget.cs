using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace topdownGame.Actions  {
    
    public class AimToTarget : MonoBehaviour  {
        public Transform Target;
        public SpriteRenderer TargetRend;
        public bool IsWeapon;
        public Vector2 AimDirection => m_targetWorldPosition -  transform.position;
        
        private Vector3 m_targetWorldPosition;
        private float m_aimAngle;
        
        private void Update()  {
            m_targetWorldPosition = Target.transform.position;
            
            if (!IsWeapon) return;

            m_aimAngle = AngleBetweenPoints(transform.position, m_targetWorldPosition);
            
            TargetRend.flipY = m_aimAngle is < -90 or > 90;

            transform.rotation =  Quaternion.Euler (new Vector3(0f,0f,m_aimAngle));
        }
        
        private float AngleBetweenPoints(Vector2 a, Vector2 b) {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }
    }
}