using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using topdownGame.Events;
using topdownGame.Managers;
using UnityEngine;

namespace topdownGame.IA {
    
    public class EnemySeekersHelper : MonoBehaviour {
        
        private List<GameObject> m_seekerEnemies = new List<GameObject>();
        public float SpaceBetween;

        private void Awake() {
            GameManager.Instance.GlobalDispatcher.Subscribe<OnEnemySeekerDeath>(OnEnemySeekerDeath);
            var enemies = FindObjectsOfType<EnemySeeker>().ToList();
            foreach (var enemy in enemies) {
                m_seekerEnemies.Add(enemy.gameObject);
            }
        }

        private void OnEnemySeekerDeath(OnEnemySeekerDeath ev) {
            m_seekerEnemies.Remove(ev.SeekerObject);
            Destroy(ev.SeekerObject);

            if (m_seekerEnemies.Count <= 0) {
                Debug.Log("acabou");
            }
        }

        public List<GameObject> GetEnemiesSeekers() {
            return m_seekerEnemies;
        }

        public float GetSpaceBetween() {
            return SpaceBetween;
        }
    }
}