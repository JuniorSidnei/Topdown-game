using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace topdownGame.Controller { 
    
    [RequireComponent(typeof(BoxCollider2D))]
    public class Controller2D : MonoBehaviour {

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
        }

        private void Update() {
            UpdateRaycastOrigins();
            CalculateRaySpacing();

            for (var i = 0; i < m_verticalRayCount; i++) {
                Debug.DrawRay(m_rayOrigins.bottomLeft + Vector2.right * m_verticalRaySpacing * i, Vector2.up * -2, Color.red);
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
