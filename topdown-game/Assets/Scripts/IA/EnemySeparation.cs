using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using topdownGame.Actions;
using UnityEngine;

namespace topdownGame.IA {
    
    public class EnemySeparation : MonoBehaviour {

        private List<EnemySeeker> m_seekerEnemies;
        public float SpaceBetween;

        private Character m_character;

        private void Awake() {
            m_seekerEnemies = FindObjectsOfType<EnemySeeker>().ToList();
            m_character = GetComponent<Character>();
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