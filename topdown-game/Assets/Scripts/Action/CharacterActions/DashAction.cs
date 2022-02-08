using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using topdownGame.Characters.Status;
using topdownGame.Events;
using topdownGame.Managers;
using UnityEngine;

namespace topdownGame.Actions {

    public class DashAction : MonoBehaviour {
        [SerializeField] private CharacterConstitution m_characterConstitution;
        [SerializeField] private float m_dashForce;
        [SerializeField] private float m_dashCooldownTimer;
        [SerializeField] private int m_dashStaminaGas;
        [SerializeField] private float m_dashRecover;
        private float m_dashCooldown;
        
        private Character m_character;
        
        private void Start() {
            m_character = GetComponent<Character>();
            
            GameManager.Instance.GlobalDispatcher.Subscribe<OnDash>(OnDash);
        }

        private void Update() {
            m_dashCooldown -= Time.deltaTime;
            if (m_dashCooldown <= 0) m_dashCooldown = 0;

            m_characterConstitution.CurrentStaminaInGame += m_dashRecover;
            if (m_characterConstitution.CurrentStaminaInGame >= m_characterConstitution.MaxStaminaInGame) {
                m_characterConstitution.CurrentStaminaInGame = m_characterConstitution.MaxStaminaInGame;
            }
        }

        private void OnDash(OnDash ev) {

            if (m_dashCooldown > 0 || m_characterConstitution.CurrentStaminaInGame <= m_dashStaminaGas) return;
            
            GameManager.Instance.GlobalDispatcher.Emit(new OnStaminaUpdate(m_dashStaminaGas));
            var to = Vector3.zero;
            var directionY = m_character.Velocity.y > 0 ? 1 : m_character.Velocity.y < 0 ? -1 : 0;
            var directionX = m_character.Velocity.x > 0 ? 1 : m_character.Velocity.x < 0 ? -1 : 0;

            to = new Vector3(m_dashForce * directionX, m_dashForce * directionY);
            Debug.Log("to: " + to);
            to += m_character.Velocity;
            DOTween.To(() => m_character.Velocity, velocity => m_character.Velocity = to, to, .1f).SetEase(Ease.Linear).OnComplete(() => {
                m_dashCooldown = m_dashCooldownTimer;
            });
        }
    }
}