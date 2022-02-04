using System;
using System.Collections;
using System.Collections.Generic;
using topdownGame.Controller;
using topdownGame.Events;
using topdownGame.Managers;
using UnityEngine;

namespace topdownGame.Utils {
    
    public class CameraFollow : MonoBehaviour {

        public Controller2D target;
        public Vector2 focusAreaSize;
        [Header("Camera settings follow")]
        public float verticalOffset;
        public float lookAheadDistanceX;
        public float lookAheadDistanceY;
        public float lookSmoothTime;

        private FocusArea m_focusArea;
        private Vector3 m_targetInput;
        
        //x settings
        private float m_currentLookAheadX;
        private float m_targetLookAheadX;
        private float m_lookAheadDirX;
        private float m_smoothLookVelocityX;
        private bool m_lookAhedStoppedX;
        
        //y settings
        private float m_currentLookAheadY;
        private float m_targetLookAheadY;
        private float m_lookAheadDirY;
        private float m_smoothLookVelocityY;
        private bool m_lookAhedStoppedY;
        
        private void Start() {
            GameManager.Instance.GlobalDispatcher.Subscribe<OnMove>(OnMove);
            m_focusArea = new FocusArea(target.boxCollider.bounds, focusAreaSize);
        }

        private void OnMove(OnMove ev) {
            m_targetInput = ev.InputDirection;
        }
        
        private void LateUpdate() {
            m_focusArea.Update(target.boxCollider.bounds);
            var focusPosition = m_focusArea.centre + Vector2.up * verticalOffset;

            if (m_focusArea.velocity.x != 0) {
                m_lookAheadDirX = Mathf.Sign(m_focusArea.velocity.x);
                if (Mathf.Sign(m_targetInput.x) == Mathf.Sign(m_focusArea.velocity.x) && m_targetInput.x != 0) {
                    m_lookAhedStoppedX = false;
                    m_targetLookAheadX = m_lookAheadDirX * lookAheadDistanceX;
                }
                else {
                    if (!m_lookAhedStoppedX) {
                        m_lookAhedStoppedX = true;
                        m_targetLookAheadX = m_currentLookAheadX + (m_lookAheadDirX * lookAheadDistanceX - m_currentLookAheadX) / 4f;
                    }
                }
            }
            if (m_focusArea.velocity.y != 0) {
                m_lookAheadDirY = Mathf.Sign(m_focusArea.velocity.y);
                if (Mathf.Sign(m_targetInput.y) == Mathf.Sign(m_focusArea.velocity.y) && m_targetInput.y != 0) {
                    m_lookAhedStoppedY = false;
                    m_targetLookAheadY = m_lookAheadDirY * lookAheadDistanceY;
                }
                else {
                    if (!m_lookAhedStoppedY) {
                        m_lookAhedStoppedY = true;
                        m_targetLookAheadY = m_currentLookAheadY + (m_lookAheadDirY * lookAheadDistanceY - m_currentLookAheadY) / 4f;
                    }
                }
            }
            
            m_currentLookAheadX = Mathf.SmoothDamp(m_currentLookAheadX, m_targetLookAheadX, ref m_smoothLookVelocityX, lookSmoothTime);
            m_currentLookAheadY = Mathf.SmoothDamp(m_currentLookAheadY, m_targetLookAheadY, ref m_smoothLookVelocityY, lookSmoothTime);
            //focusPosition.y = Mathf.SmoothDamp(transform.position.y, focusPosition.y, ref m_smoothLookVelocityY, lookSmoothTime);
            focusPosition += Vector2.one * (m_currentLookAheadX + m_currentLookAheadY);
            
            transform.position = (Vector3)focusPosition + Vector3.forward * -10;
        }

        struct FocusArea {
            public Vector2 centre;
            public Vector2 velocity;
            private float top, bottom, left, right;

            public FocusArea(Bounds targetBounds, Vector2 size) {
                left = targetBounds.center.x - size.x/2;
                right = targetBounds.center.x + size.x/2;
                bottom = targetBounds.min.y;
                top = targetBounds.min.y + size.y;

                centre = new Vector2((left + right) / 2, (top + bottom)/8);
                velocity = Vector2.zero;
            }

            public void Update(Bounds targetBounds) {
                float shiftX = 0;
                float shiftY = 0;
                
                if (targetBounds.min.x < left) {
                    shiftX = targetBounds.min.x - left;
                } else if (targetBounds.max.x > right) {
                    shiftX = targetBounds.max.x - right;
                }

                if (targetBounds.min.y < bottom) {
                    shiftY = targetBounds.min.x - bottom;
                } else if (targetBounds.max.y > top) {
                    shiftY = targetBounds.max.x - top;
                }
                
                left += shiftX;
                right += shiftX;
                top += shiftY;
                bottom += shiftY;
                
                centre = new Vector2((left + right) / 2, (top + bottom) / 8);
                velocity = new Vector2(shiftX, shiftY);
            } 
        }
        
        private void OnDrawGizmos() {
            Gizmos.color = new Color (1, 0, 0, .5f);
            Gizmos.DrawCube (m_focusArea.centre, focusAreaSize);
        }
    }
}