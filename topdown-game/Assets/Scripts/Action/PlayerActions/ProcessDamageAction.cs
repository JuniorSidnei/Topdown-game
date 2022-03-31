using DG.Tweening;
using topdownGame.Events;
using topdownGame.Interfaces;
using topdownGame.Managers;
using UnityEngine;

namespace topdownGame.Actions {
    
    public class ProcessDamageAction : MonoBehaviour, IDamageable {
        
        public Vector2 KnockbackForce;
        public float DamageCooldown;
        public bool IsPropObject;
        private Character m_character;

        private float m_damageCooldown;

        private void Start() {
            m_character = GetComponent<Character>();
            m_damageCooldown = DamageCooldown;
        }

        private void Update() {
            m_damageCooldown -= Time.deltaTime;
            if (m_damageCooldown <= 0) m_damageCooldown = 0;
        }

        public void Damage(OnBulletHit.EmitterInfo emitter, OnBulletHit.ReceiverInfo receiver, bool destroyEmitterImmediately)  {
            if (receiver.Character != m_character || m_damageCooldown > 0 || gameObject == null) return;

            if (IsPropObject) {
                Destroy(gameObject);
                return;
            }
            
            m_damageCooldown = DamageCooldown;
            var directionX = 0;
            var directionY = 0;

            if (emitter.Object == null) return;
            
            if (emitter.Object.transform.position.x < receiver.Object.transform.position.x) {
                directionX = 1;
            } else if (emitter.Object.transform.position.x > receiver.Object.transform.position.x) {
                directionX = -1;
            }

            if (emitter.Object.transform.position.y < receiver.Object.transform.position.y) {
                directionY = 1;
            } else if (emitter.Object.transform.position.y > receiver.Object.transform.position.y) {
                directionY = -1;
            }
            
            GameManager.Instance.GlobalDispatcher.Emit(new OnDamageText(transform.position, emitter.Damage));
            var emitterDamage = emitter.Damage;

            if (destroyEmitterImmediately) {
                Destroy(emitter.Object);
            }

            var to = new Vector3(KnockbackForce.x * directionX, KnockbackForce.y * directionY);
            to += m_character.Velocity;
            DOTween.To(() => m_character.Velocity, velocity => m_character.Velocity = to, to, .1f).SetEase(Ease.Linear).OnComplete(() => {
                GameManager.Instance.GlobalDispatcher.Emit(new OnLifeUpdate(m_character, emitterDamage));
            });
        }
    }
}