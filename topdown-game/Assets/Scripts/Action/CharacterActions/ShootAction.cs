using System.Collections;
using System.Collections.Generic;
using topdownGame.Events;
using topdownGame.Managers;
using topdownGame.Weapons;
using topdownGame.Weapons.Pistol;
using UnityEngine;

namespace topdownGame.Actions
{
    public class ShootAction : MonoBehaviour
    {
        // public GameObject BulletOj;
        // public Transform AimTransform;
        // public Transform SpawnTransform;

        public Transform WeaponHolder;
        private float m_fireCooldown;
        private Weapon m_currentWeapon;
        
        private void Start()
        {
            GameManager.Instance.GlobalDispatcher.Subscribe<OnFire>(OnFire);
            GameManager.Instance.GlobalDispatcher.Subscribe<OnPickedWeapon>(OnPickedWeapon);
            m_currentWeapon = WeaponHolder.GetComponentInChildren<Weapon>();
            
            if (!m_currentWeapon) return;
            m_fireCooldown = m_currentWeapon.WeaponsData.FireCooldown;
        }

        private void OnPickedWeapon(OnPickedWeapon ev) {
            if(m_currentWeapon) DestroyImmediate(m_currentWeapon.gameObject);
            
            m_currentWeapon = ev.Weapon.GetComponent<Weapon>();
        }
        
        private void OnFire(OnFire ev) {
            if (m_fireCooldown > 0 || m_currentWeapon == null) return;
            
            m_currentWeapon.Shoot();
            m_fireCooldown = m_currentWeapon.WeaponsData.FireCooldown;
        }
        
        private void Update()
        {
            m_fireCooldown -= Time.deltaTime;
            if (m_fireCooldown <= 0) m_fireCooldown = 0;
        }
    }
}
