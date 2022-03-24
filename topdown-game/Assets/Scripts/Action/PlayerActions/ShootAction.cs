using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using topdownGame.Events;
using topdownGame.Managers;
using topdownGame.Weapons;
using topdownGame.Weapons.Pistol;
using UnityEngine;

namespace topdownGame.Actions {
    public class ShootAction : MonoBehaviour {
        
        public Transform WeaponRightHolder;
        public Transform WeaponLeftHolder;
        
        protected float m_fireCooldown;
        
        protected bool m_isFiring;
        protected bool m_isReloading;
        protected float m_reloadTime;
        
        protected void UpdateFireCooldown() {
            m_fireCooldown -= Time.deltaTime;
            if (m_fireCooldown <= 0) m_fireCooldown = 0;
        }

        protected void ShootingAndReloading(Weapon currentWeapon)  {
            Shoot(currentWeapon);

            if (!m_isReloading) return;
            
            m_reloadTime -= Time.deltaTime;
            if (!(m_reloadTime <= 0)) return;
            
            m_reloadTime = currentWeapon.WeaponsData.TimeToReload;
            m_isReloading = false;
            currentWeapon.Reload();
        }
        
        private void Shoot(Weapon currentWeapon) {
            if (m_fireCooldown > 0 || currentWeapon == null || !m_isFiring || m_isReloading) return;

            if (!currentWeapon.CanShoot()) {
                m_isReloading = true;
                return;
            }
            
            currentWeapon.Shoot(gameObject.layer);
            m_fireCooldown = currentWeapon.WeaponsData.FireCooldown;
        }
    }
}
