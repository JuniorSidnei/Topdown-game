using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using topdownGame.Actions;
using topdownGame.Events;
using topdownGame.Managers;
using UnityEngine;

namespace topdownGame.IA {
    
    public class EnemySeparation : MonoBehaviour {

        private List<GameObject> m_seekerEnemies = new();
        public float SpaceBetween;

        private Character m_character;

        private void Awake() {
            GameManager.Instance.GlobalDispatcher.Subscribe<OnEnemySeekerDeath>(OnEnemySeekerDeath);
            var enemies = FindObjectsOfType<EnemySeeker>().ToList();
            foreach (var enemy in enemies) {
                m_seekerEnemies.Add(enemy.gameObject);
            }
            m_character = GetComponent<Character>();
        }

        private void OnEnemySeekerDeath(OnEnemySeekerDeath ev) {
            m_seekerEnemies.Remove(ev.SeekerObject);
        }
        
        private void Update() {
            foreach (var seeker in m_seekerEnemies) {
                if (seeker.GetComponent<Character>() == m_character) continue;
                
                var distanceToOther = Vector3.Distance(transform.position, seeker.transform.position);
                if (!(distanceToOther <= SpaceBetween)) continue;
                    
                var dir = transform.position - seeker.transform.position;
                var speed = seeker.GetComponent<EnemySeeker>().speed;
                var velocity = dir * speed * Time.deltaTime;
                m_character.Velocity += velocity;
            }
        }
    }
}