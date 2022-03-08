using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using topdownGame.Actions;
using topdownGame.Controller;
using UnityEngine;

namespace topdownGame.IA  {
    
    public class EnemySeeker : MonoBehaviour  {
        public Transform Target;
        public Path Path;

        public float speed;
        public float nextWaypointDistance;
        private bool m_reachedEndOfPath;
        
        private Character m_character;
        private Seeker m_seeker;
        private int m_currentWaypoint = 0;
        private Vector3 m_velocitySmoothing;

        public delegate void OnReachedTarget(Character character, bool canShoot);
        public static event OnReachedTarget OnReached;
        
        private void Awake() {
            m_character = GetComponent<Character>();
            m_seeker = GetComponent<Seeker>();

            m_seeker.pathCallback += OnPathComplete;
        }

        private void Start() {
            InvokeRepeating(nameof(UpdatePath), 0f, 0.5f);
        }

        private void UpdatePath() {
            if (m_seeker.IsDone()) {
                m_seeker.StartPath(transform.position, Target.position);    
            }
        }


        private void OnPathComplete(Path p) {
            if (p.error) return;
            
            Path = p;
            m_currentWaypoint = 0;
        }
        
        private void FixedUpdate() {
            if (Path == null) return;

            m_reachedEndOfPath = false;
            
            if (m_currentWaypoint >= Path.vectorPath.Count) {
                m_reachedEndOfPath = true;
                OnReached?.Invoke(m_character, true);
                return;
            }
            
            OnReached?.Invoke(m_character, false);
            
            var dir = (Path.vectorPath[m_currentWaypoint] - transform.position).normalized;
            var velocity = dir * speed * Time.deltaTime;
            m_character.Velocity += velocity;

            var distanceToWaypoint = Vector3.Distance(transform.position, Path.vectorPath[m_currentWaypoint]);

            if (distanceToWaypoint < nextWaypointDistance) {
                m_currentWaypoint++;
            }
        }
    }
}