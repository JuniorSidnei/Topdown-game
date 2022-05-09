using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using topdownGame.Events;
using topdownGame.IA;
using topdownGame.Interfaces;
using topdownGame.Managers;
using topdownGame.Weapons.Info;
using UnityEditor;
using UnityEngine;

namespace topdownGame.Actions {
    
    public class EnemyOnContactExplode : MonoBehaviour
    {

        public float ExplosionRadius;
        public float ExplosionMaxDistance;
        public LayerMask TargetLayer;
        public int ExplosionDamage;
        
        private Character m_character;
        private bool m_isMakingAction;
        


        private void OnEnable() {
            EnemySeeker.OnReached += ExplodeOnContact;
        }

        private void OnDisable() {
            EnemySeeker.OnReached -= ExplodeOnContact;
        }

        private void Awake()
        {
            m_character = GetComponent<Character>();
        }

        private void ExplodeOnContact(Character character, bool canMakeAction) {
            if(character != m_character || m_isMakingAction) return;

            m_isMakingAction = canMakeAction;
            
            var results = Physics2D.CircleCastAll(transform.position, ExplosionRadius, transform.right, ExplosionMaxDistance, TargetLayer);
            GameManager.Instance.GlobalDispatcher.Emit(new OnCameraScreenShake(10, 0.15f));
            if (results.Length == 0) return;

            foreach (var hit in results) {
                var hitObject = hit.collider.gameObject;
                var emitterInfo = new OnBulletHit.EmitterInfo {
                    Damage = ExplosionDamage, Object = gameObject
                };
            
                var receiverInfo = new OnBulletHit.ReceiverInfo {
                    Object = hitObject, Character = hitObject.GetComponent<Character>()
                };
            
                hitObject.GetComponent<IDamageable>().Damage(emitterInfo, receiverInfo);
            }
            
            GameManager.Instance.GlobalDispatcher.Emit(new OnEnemySeekerDeath(gameObject));
        }
    }
}