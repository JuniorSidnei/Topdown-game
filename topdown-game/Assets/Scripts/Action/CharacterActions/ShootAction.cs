using System.Collections;
using System.Collections.Generic;
using topdownGame.Events;
using topdownGame.Managers;
using topdownGame.Weapons;
using topdownGame.Weapons.Pistol;
using UnityEngine;

namespace topdownGame.Actions {
    public class ShootAction : MonoBehaviour {

        public Transform WeaponHolder;
        private float m_fireCooldown;
        public Weapon m_currentWeapon;
        
        private void Start() {
            GameManager.Instance.GlobalDispatcher.Subscribe<OnFire>(OnFire);
            m_currentWeapon = WeaponHolder.GetComponentInChildren<Weapon>();
            
            if (!m_currentWeapon) return;
            m_fireCooldown = m_currentWeapon.WeaponsData.FireCooldown;
        }

        private void OnFire(OnFire ev) {
            if (m_fireCooldown > 0 || m_currentWeapon == null) return;
            
            m_currentWeapon.Shoot();
            m_fireCooldown = m_currentWeapon.WeaponsData.FireCooldown;
        }
        
        private void Update() {
            m_fireCooldown -= Time.deltaTime;
            if (m_fireCooldown <= 0) m_fireCooldown = 0;
        }
        
        public void SetPickedWeapon(Weapon weapon) {
            m_currentWeapon = weapon;
        }
    }
}
