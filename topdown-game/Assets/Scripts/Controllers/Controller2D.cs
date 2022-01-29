using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace topdownGame.Controller { 
    
    [RequireComponent(typeof(BoxCollider2D))]
    public class Controller2D : MonoBehaviour {

        public LayerMask collisionMask;
        
        struct RaycastOrigins {
            public Vector2 topLeft, topRight;
            public Vector2 bottomLeft, bottomRight;
        }
        
        private BoxCollider2D m_boxCollider;
        private RaycastOrigins m_rayOrigins;

        private const float m_skinWidth = 0.015f;
        private float m_horizontalRaySpacing;
        private float m_verticalRaySpacing;
        [SerializeField] private int m_horizontalRayCount = 4;
        [SerializeField] private int m_verticalRayCount = 4;

        private void Awake() {
            m_boxCollider = GetComponent<BoxCollider2D>();
            CalculateRaySpacing();
        }

        
        public void Move(Vector3 velocity) {
            UpdateRaycastOrigins();
            HorizontalCollisions(ref velocity);
            VerticalCollisions(ref velocity);
            
            transform.Translate(velocity);    
        }
        
        private void VerticalCollisions(ref Vector3 velocity) {
            var directionY = Mathf.Sign(velocity.y);
            var rayLenght = Mathf.Abs(velocity.y) + m_skinWidth;
            
            for (var i = 0; i < m_verticalRayCount; i++) {
                var rayOrigin = (directionY == -1) ? m_rayOrigins.bottomLeft : m_rayOrigins.topLeft;
                rayOrigin += Vector2.right * (m_horizontalRaySpacing * i + velocity.x);
                var hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLenght, collisionMask);

                if (hit) {
                    velocity.y = (hit.distance - m_skinWidth) * directionY;
                    rayLenght = hit.distance;
                    Debug.Log("bateu vertical");
                }
                
                Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLenght, Color.red);
            }
        }

        private void HorizontalCollisions(ref Vector3 velocity) {
            var directionX = Mathf.Sign(velocity.x);
            var rayLenght = Mathf.Abs(velocity.x) + m_skinWidth;
            
            for (var i = 0; i < m_horizontalRayCount; i++) {
                var rayOrigin = (directionX == 1) ? m_rayOrigins.bottomLeft : m_rayOrigins.bottomRight;
                rayOrigin += Vector2.up * (m_verticalRaySpacing * i);
                var hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLenght, collisionMask);

                if (hit) {
                    velocity.y = (hit.distance - m_skinWidth) * directionX;
                    rayLenght = hit.distance;
                    Debug.Log("bateu horizontal");
                }
                
                Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLenght, Color.red);
            }
        }
        
        
        private void UpdateRaycastOrigins() {
            Bounds bounds = m_boxCollider.bounds;
            bounds.Expand(m_skinWidth * -2);
            
            m_rayOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
            m_rayOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
            m_rayOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
            m_rayOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        }

        private void CalculateRaySpacing() {
            Bounds bounds = m_boxCollider.bounds;
            bounds.Expand(m_skinWidth * -2);

            m_horizontalRayCount = Mathf.Clamp(m_horizontalRayCount, 2, int.MaxValue);
            m_verticalRayCount = Mathf.Clamp(m_verticalRayCount, 2, int.MaxValue);
            
            m_horizontalRaySpacing = bounds.size.y / (m_horizontalRayCount - 1);
            m_verticalRaySpacing = bounds.size.x / (m_verticalRayCount - 1);
            
        }
    }
}
