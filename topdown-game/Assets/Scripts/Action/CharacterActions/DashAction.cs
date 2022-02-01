using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using topdownGame.Events;
using topdownGame.Managers;
using UnityEngine;

namespace topdownGame.Actions {

    public class DashAction : MonoBehaviour {
        [SerializeField] private float m_dashForce;
        
        private Character m_character;
        
        private void Start() {
            m_character = GetComponent<Character>();
            
            GameManager.Instance.GlobalDispatcher.Subscribe<OnDash>(OnDash);
        }

        private void OnDash(OnDash ev) {
            Vector3 to;
            var directionY = Mathf.Sign(m_character.Velocity.y);
            var directionX = Mathf.Sign(m_character.Velocity.x);
            
            if (m_character.Velocity.y == 0) {
                to = new Vector3(m_dashForce * directionX, 0);
            } else if (m_character.Velocity.x == 0) {
                to = new Vector3(0, m_dashForce * directionY);
            }
            else {
                to = new Vector3(m_dashForce * directionX, m_dashForce * directionY);
            }

            to += m_character.Velocity;
            DOTween.To(() => m_character.Velocity, velocity => m_character.Velocity = to, to, .1f).SetEase(Ease.Linear).OnComplete(() => {
                
            });
        }
    }
}