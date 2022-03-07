using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using topdownGame.Actions;
using topdownGame.Events;
using topdownGame.Managers;
using topdownGame.Weapons;
using UnityEngine;

namespace topdownGame.Actions  {
    
    public class PlayerShoot : ShootAction  {
        private Weapon m_currentWeapon;
        private AimAction m_aimAction;

        private void Awake() {
            GameManager.Instance.GlobalDispatcher.Subscribe<OnFire>(OnFire);
        }

        private void OnFire(OnFire ev) {
            m_isFiring = ev.Firing;
        }
        
        public void SetPickedWeapon(Weapon weapon) {
            m_currentWeapon = weapon;
            m_fireCooldown = m_currentWeapon.WeaponsData.FireCooldown;
            m_reloadTime = m_currentWeapon.WeaponsData.TimeToReload;
            m_currentWeapon.Reload();
            m_aimAction = m_currentWeapon.GetComponent<AimAction>();
        }
        
        private void Update() {
            if (!m_aimAction) return;
            
            if (m_aimAction.IsWeaponFlipped()) {
                m_currentWeapon.transform.SetParent(WeaponRightHolder);
                m_currentWeapon.transform.DOLocalMove(Vector3.zero, 0.1f);
            }
            else  {
                m_currentWeapon.transform.SetParent(WeaponLeftHolder);
                m_currentWeapon.transform.DOLocalMove(Vector3.zero, 0.1f);
            }
            
            UpdateFireCooldown();
            ShootingAndReloading(m_currentWeapon);
        }
    }
}