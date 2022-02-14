using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace topdownGame.Controller  {
    
    [RequireComponent(typeof(BoxCollider2D))]
    public class RaycastController2D : MonoBehaviour  {
        
        public LayerMask collisionMask;
        public CollisionsInfo collisionsInfo;
        
        [HideInInspector]
        public BoxCollider2D boxCollider;
        public RaycastOrigins m_rayOrigins;
        
        public const float m_skinWidth = 0.15f;
        [HideInInspector]
        public float m_horizontalRaySpacing;
        [HideInInspector]
        public float m_verticalRaySpacing;
        public int m_horizontalRayCount = 4;
        public int m_verticalRayCount = 4;
        
        public struct RaycastOrigins {
            public Vector2 TopLeft, TopRight;
            public Vector2 BottomLeft, BottomRight;
        }

        public struct CollisionsInfo {
            public bool Above, Bellow, Left, Right;
            public GameObject ObjectCollider;
            
            public void ResetCollisions() {
                Above = Bellow = false;
                Left = Right = false;
                ObjectCollider = null;
            }

            public bool HasCollision() {
                return Above || Bellow || Right || Left;
            }
        }
        
        private void Awake() {
            boxCollider = GetComponent<BoxCollider2D>();
            CalculateRaySpacing();
        }
        
        public void UpdateRaycastOrigins() {
            Bounds bounds = boxCollider.bounds;
            bounds.Expand(m_skinWidth * -2);

            m_rayOrigins.TopLeft = new Vector2(bounds.min.x, bounds.max.y);
            m_rayOrigins.TopRight = new Vector2(bounds.max.x, bounds.max.y);
            m_rayOrigins.BottomRight = new Vector2(bounds.max.x, bounds.min.y);
            m_rayOrigins.BottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        }

        public void CalculateRaySpacing() {
            Bounds bounds = boxCollider.bounds;
            bounds.Expand(m_skinWidth * -2);

            m_horizontalRayCount = Mathf.Clamp(m_horizontalRayCount, 2, int.MaxValue);
            m_verticalRayCount = Mathf.Clamp(m_verticalRayCount, 2, int.MaxValue);

            m_horizontalRaySpacing = bounds.size.y / (m_horizontalRayCount - 1);
            m_verticalRaySpacing = bounds.size.x / (m_verticalRayCount - 1);

        }
    }
}