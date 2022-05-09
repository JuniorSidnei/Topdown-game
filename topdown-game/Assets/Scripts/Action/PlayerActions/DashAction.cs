using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using topdownGame.Characters.Status;
using topdownGame.Events;
using topdownGame.Hud;
using topdownGame.Managers;
using UnityEngine;

namespace topdownGame.Actions {

    public class DashAction : MonoBehaviour {
        [SerializeField] private CharacterConstitution m_characterConstitution;
        [SerializeField] private float m_dashForce;
        [SerializeField] private float m_dashCooldownTimer;
        [SerializeField] private int m_dashStaminaGas;
        [SerializeField] private float m_dashRecover;
        [SerializeField] private float m_dragDash;
        private float m_dashCooldown;
        
        private Character m_character;
        private Vector2 m_inputDirection;
        
        private void Start() {
            m_character = GetComponent<Character>();
            
            GameManager.Instance.GlobalDispatcher.Subscribe<OnDash>(OnDash);
            GameManager.Instance.GlobalDispatcher.Subscribe<OnMove>(OnMove);
        }

        private void Update() {
            m_dashCooldown -= Time.deltaTime;
            if (m_dashCooldown <= 0) m_dashCooldown = 0;
            
            m_characterConstitution.CurrentStaminaInGame += m_dashRecover;
            if (m_characterConstitution.CurrentStaminaInGame >= m_characterConstitution.MaxStaminaInGame) {
                m_characterConstitution.CurrentStaminaInGame = m_characterConstitution.MaxStaminaInGame;
            }
            
            HudManager.Instance.UpdateStaminaBar(m_characterConstitution.CurrentStaminaInGame / m_characterConstitution.MaxStamina);
        }

        private void OnMove(OnMove ev) {
            m_inputDirection = ev.InputDirection;
        }
        
        private void OnDash(OnDash ev) {

            if (m_dashCooldown > 0 || m_characterConstitution.CurrentStaminaInGame <= m_dashStaminaGas) return;
            
            m_characterConstitution.CurrentStaminaInGame -= m_dashStaminaGas;
            var to = Vector3.zero;
            
            var directionX = m_inputDirection.x;
            var directionY = m_inputDirection.y;
            
            to = new Vector3(m_dashForce * directionX, m_dashForce * directionY);
            to += m_character.Velocity * -m_dragDash;
            DOTween.To(() => m_character.Velocity, velocity => m_character.Velocity = to, to, .15f).SetEase(Ease.Linear).OnComplete(() => {
                m_dashCooldown = m_dashCooldownTimer;
            });
        }
    }
}