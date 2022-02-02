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
            var to = Vector3.zero;
            var directionY = m_character.Velocity.y > 0 ? 1 : m_character.Velocity.y < 0 ? -1 : 0;
            var directionX = m_character.Velocity.x > 0 ? 1 : m_character.Velocity.x < 0 ? -1 : 0;

            to = new Vector3(m_dashForce * directionX, m_dashForce * directionY);
            to += m_character.Velocity;
            DOTween.To(() => m_character.Velocity, velocity => m_character.Velocity = to, to, .1f).SetEase(Ease.Linear).OnComplete(() => {
                
            });
        }
    }
}