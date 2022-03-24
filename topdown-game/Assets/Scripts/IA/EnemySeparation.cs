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

        public EnemySeekersHelper EnemySeekersHelper;
        private Character m_character;
        
        private void Awake() {
            m_character = GetComponent<Character>();
        }
        
        private void Update() {
            foreach (var seeker in EnemySeekersHelper.GetEnemiesSeekers()) {
                if (seeker.GetComponent<Character>() == m_character) continue;
                
                var distanceToOther = Vector3.Distance(transform.position, seeker.transform.position);
                if (!(distanceToOther <= EnemySeekersHelper.GetSpaceBetween())) continue;
                    
                var dir = transform.position - seeker.transform.position;
                var speed = seeker.GetComponent<EnemySeeker>().speed;
                var velocity = dir * speed * Time.deltaTime;
                m_character.Velocity += velocity;
            }
        }
    }
}