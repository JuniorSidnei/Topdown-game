using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using topdownGame.Events;
using topdownGame.Hud;
using topdownGame.Managers;
using topdownGame.Weapons;
using topdownGame.Weapons.Pistol;
using UnityEngine;
using UnityEngine.UI;

namespace topdownGame.Actions {
    public class ShootAction : MonoBehaviour {
        
        [Header("holders")]
        public Transform WeaponRightHolder;
        public Transform WeaponLeftHolder;

        [Header("reload feedback canvas")]
        public GameObject ReloadContainer;
        public Image ReloadImg;
        
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
            // HudManager.Instance.UpdateBulletAmount(currentWeapon.WeaponsData);
            
            if (!m_isReloading) return;
            
            m_reloadTime -= Time.deltaTime;
            
            if (ReloadImg) {
                ReloadImg.fillAmount = 0f;
                ReloadContainer.SetActive(true);
                ReloadImg.DOFillAmount(1, currentWeapon.WeaponsData.TimeToReload);
            }

            if (!(m_reloadTime <= 0)) return;

            m_reloadTime = currentWeapon.WeaponsData.TimeToReload;
            m_isReloading = false;
            
            if (ReloadContainer) {
                ReloadContainer.SetActive(false);
            }

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
