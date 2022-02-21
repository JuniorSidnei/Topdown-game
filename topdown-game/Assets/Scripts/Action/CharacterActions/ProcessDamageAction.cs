using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using topdownGame.Events;
using topdownGame.Managers;
using topdownGame.Utils;
using UnityEngine;

namespace topdownGame.Actions
{
    
    public class ProcessDamageAction : MonoBehaviour {
        
        public LayerMask DamageContact;
        public Vector2 KnockbackForce;
        public float DamageCooldown;
        private Character m_character;

        private float m_damageCooldown;
        
        private void Start()
        {
            m_character = GetComponent<Character>();
            GameManager.Instance.GlobalDispatcher.Subscribe<OnSimpleBulletHit>(OnSimpleBulletHit);
            m_damageCooldown = DamageCooldown;
        }

        private void Update() {
            m_damageCooldown -= Time.deltaTime;
            if (m_damageCooldown <= 0) m_damageCooldown = 0;
        }

        private void OnSimpleBulletHit(OnSimpleBulletHit ev)
        {
            if (ev.Receiver.Character != m_character || m_damageCooldown > 0) return;
            
            m_damageCooldown = DamageCooldown;
            var directionX = 0;
            var directionY = 0;

            if (ev.Emitter.EmitterObject == null) return;
            
            if (ev.Emitter.EmitterObject.transform.position.x < ev.Receiver.ReceiverObject.transform.position.x) {
                directionX = 1;
            } else if (ev.Emitter.EmitterObject.transform.position.x > ev.Receiver.ReceiverObject.transform.position.x) {
                directionX = -1;
            }

            if (ev.Emitter.EmitterObject.transform.position.y < ev.Receiver.ReceiverObject.transform.position.y) {
                directionY = 1;
            } else if (ev.Emitter.EmitterObject.transform.position.y > ev.Receiver.ReceiverObject.transform.position.y) {
                directionY = -1;
            }
            
            var to = Vector3.zero;
            to = new Vector3(KnockbackForce.x * directionX, KnockbackForce.y * directionY);
            to += m_character.Velocity;
            DOTween.To(() => m_character.Velocity, velocity => m_character.Velocity = to, to, .1f).SetEase(Ease.Linear).OnComplete(() => {
                GameManager.Instance.GlobalDispatcher.Emit(new OnLifeUpdate(m_character, ev.Emitter.EmitterDamage));
            });
        }
    }
}