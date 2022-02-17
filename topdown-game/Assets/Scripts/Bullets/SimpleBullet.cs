using System.Collections;
using System.Collections.Generic;
using topdownGame.Actions;
using topdownGame.Controller;
using topdownGame.Events;
using topdownGame.Managers;
using topdownGame.Utils;
using UnityEngine;

namespace topdownGame.Bullets
{

    public class SimpleBullet : MonoBehaviour
    {
        private Controller2D m_controller;
        public int Damage;
        public float Speed;
        [SerializeField] private Collision2DProxy m_collisionProxy;
        
        private void Awake() {
            m_controller = GetComponent<Controller2D>();
            m_collisionProxy.OnTrigger2DEnterCallback.AddListener(OnTrigger2DEnterCallback);
        }

        private void OnTrigger2DEnterCallback(Collider2D ev) {
            var emitterInfo = new OnSimpleBulletHit.OnHitEmitterInfo {
                EmitterDamage = Damage, EmitterObject = gameObject
            };
            
            var receiverInfo = new OnSimpleBulletHit.OnHitReceiverInfo {
                ReceiverObject = ev.gameObject, Character = ev.gameObject.GetComponent<Character>()
            };
            
            GameManager.Instance.GlobalDispatcher.Emit(new OnSimpleBulletHit(emitterInfo, receiverInfo));
        }
        
        private void FixedUpdate() {
            if (m_controller.collisionsInfo.HasCollision()) {
                DestroyImmediate(gameObject);
                return;
            }

            var velocity = Vector2.left * Speed;
            m_controller.Move(velocity * Time.deltaTime);
        }
    }
}