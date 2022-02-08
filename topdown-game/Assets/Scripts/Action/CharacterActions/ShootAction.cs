using System.Collections;
using System.Collections.Generic;
using topdownGame.Events;
using topdownGame.Managers;
using UnityEngine;

namespace topdownGame.Actions
{
    public class ShootAction : MonoBehaviour
    {
        public GameObject BulletOj;
        public Transform AimTransform;
        public Transform SpawnTransform;

        public float FireCooldown;
        private float m_fireCooldown;
        
        private void Start()
        {
            GameManager.Instance.GlobalDispatcher.Subscribe<OnFire>(OnFire);
            m_fireCooldown = FireCooldown;
        }

        private void OnFire(OnFire ev) {
            if (m_fireCooldown > 0) return;

            var tempBullet = Instantiate(BulletOj, SpawnTransform.position, Quaternion.identity);
            tempBullet.transform.right = AimTransform.right;
            m_fireCooldown = FireCooldown;
        }
        
        private void Update()
        {
            m_fireCooldown -= Time.deltaTime;
            if (m_fireCooldown <= 0) m_fireCooldown = 0;
        }
    }
}
