using System.Collections;
using System.Collections.Generic;
using topdownGame.Actions;
using topdownGame.Controller;
using topdownGame.Events;
using topdownGame.Interfaces;
using topdownGame.Managers;
using topdownGame.Utils;
using topdownGame.Weapons.Info;
using UnityEngine;

namespace topdownGame.Bullets {
    
    public class SimpleBullet : MonoBehaviour {
        
        public LayerMask ObstacleLayer;
        private Controller2D m_controller;
        public WeaponsData WeaponsData;
        public float Speed;
        [SerializeField] private Collision2DProxy m_collisionProxy;
        private LayerMask m_ownerLayer;
        
        private void Awake() {
            m_controller = GetComponent<Controller2D>();
            m_collisionProxy.OnTrigger2DEnterCallback.AddListener(OnTrigger2DEnterCallback);
        }

        private void OnTrigger2DEnterCallback(Collider2D ev) {
            if(((1 << ev.gameObject.layer) & ObstacleLayer) == 0) {
                return;
            }

            if (ev.gameObject.layer == m_ownerLayer) return;

            var emitterInfo = new OnBulletHit.EmitterInfo {
                Damage = WeaponsData.Damage, Object = gameObject
            };
            
            var receiverInfo = new OnBulletHit.ReceiverInfo {
                Object = ev.gameObject, Character = ev.gameObject.GetComponent<Character>()
            };
            
            ev.GetComponent<IDamageable>()?.Damage(emitterInfo, receiverInfo);
            //GameManager.Instance.GlobalDispatcher.Emit(new OnBulletHit(emitterInfo, receiverInfo));
        }
        
        private void FixedUpdate() {
            if (m_controller.collisionsInfo.HasCollision()) {
                DestroyImmediate(gameObject);
                return;
            }

            var velocity = Vector2.left * Speed;
            m_controller.Move(velocity * Time.deltaTime);
        }

        public void SetOwnerLayer(LayerMask ownerLayer) {
            m_ownerLayer = ownerLayer;
        }
    }
}