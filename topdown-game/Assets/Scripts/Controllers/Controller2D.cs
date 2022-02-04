using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace topdownGame.Controller { 
    
    public class Controller2D : RaycastController2D {
        
        public void Move(Vector3 velocity) {
            UpdateRaycastOrigins();
            collisionsInfo.ResetCollisions();
            HorizontalCollisions(ref velocity);
            VerticalCollisions(ref velocity);
            
            transform.Translate(velocity);    
        }
        
        private void VerticalCollisions(ref Vector3 velocity) {
            var directionY = Mathf.Sign(velocity.y);
            var rayLenght = Mathf.Abs(velocity.y) + m_skinWidth;
            
            for (var i = 0; i < m_verticalRayCount; i++) {
                var rayOrigin = (directionY == -1) ? m_rayOrigins.BottomLeft : m_rayOrigins.TopLeft;
                rayOrigin += Vector2.right * (m_verticalRaySpacing * i + velocity.x);
                var hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLenght, collisionMask);

                if (hit) {
                    velocity.y = (hit.distance - m_skinWidth) * directionY;
                    rayLenght = hit.distance;

                    collisionsInfo.Above = directionY == -1;
                    collisionsInfo.Bellow = directionY == 1;
                }
                
                Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLenght, Color.red);
            }
        }

        private void HorizontalCollisions(ref Vector3 velocity) {
            var directionX = Mathf.Sign(velocity.x);
            var rayLenght = Mathf.Abs(velocity.x) + m_skinWidth;
            
            for (var i = 0; i < m_horizontalRayCount; i++) {
                var rayOrigin = (directionX == -1) ? m_rayOrigins.BottomLeft : m_rayOrigins.BottomRight;
                rayOrigin += Vector2.up * (m_horizontalRaySpacing * i);
                var hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLenght, collisionMask);

                if (hit) {
                    velocity.x = (hit.distance - m_skinWidth) * directionX;
                    rayLenght = hit.distance;
                    
                    collisionsInfo.Left = directionX == -1;
                    collisionsInfo.Right = directionX == 1;
                    
                }
                
                Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLenght, Color.red);
            }
        }
    }
}
