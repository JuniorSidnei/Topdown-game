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
        public float minimumTargetDistance;
        [Range(0, 15)]
        public int waypointDistanceOffset;
        
        private Character m_character;
        private Seeker m_seeker;
        private int m_currentWaypoint = 0;
        private Vector3 m_velocitySmoothing;
        private float m_targetDistance;
        private bool m_reachedEndOfPath;
        
        public delegate void OnReachedTarget(Character character, bool canMakeAction);
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

            m_targetDistance = Vector3.Distance(transform.position, Target.transform.position);
            if (m_reachedEndOfPath) {
                if (m_targetDistance > minimumTargetDistance) {
                    m_reachedEndOfPath = false;
                    OnReached?.Invoke(m_character, false);
                }
                else {
                    OnReached?.Invoke(m_character, true);
                    return;
                }
            }

            if (m_currentWaypoint >= Path.vectorPath.Count - waypointDistanceOffset) {
                m_reachedEndOfPath = true;
                return;
            }
            
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