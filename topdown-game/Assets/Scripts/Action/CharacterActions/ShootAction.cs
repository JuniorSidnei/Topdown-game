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
        
        private float m_fireCooldown;
        private Weapon m_currentWeapon;
        private AimAction m_aimAction;
        
        private bool m_isFiring;
        private bool m_isReloading;
        private float m_reloadTime;
        
        private void Start() {
            GameManager.Instance.GlobalDispatcher.Subscribe<OnFire>(OnFire);
        }

        private void OnFire(OnFire ev) {
            m_isFiring = ev.Firing;
        }

        private void Update() {
            m_fireCooldown -= Time.deltaTime;
            if (m_fireCooldown <= 0) m_fireCooldown = 0;

            if (m_aimAction) {
                if (m_aimAction.IsWeaponFlipped())
                {
                    m_currentWeapon.transform.SetParent(WeaponRightHolder);
                    m_currentWeapon.transform.DOLocalMove(Vector3.zero, 0.1f);
                }
                else
                {
                    m_currentWeapon.transform.SetParent(WeaponLeftHolder);
                    m_currentWeapon.transform.DOLocalMove(Vector3.zero, 0.1f);
                }
            }

            Shoot();

            if (!m_isReloading) return;
            
            m_reloadTime -= Time.deltaTime;
            if (!(m_reloadTime <= 0)) return;
            
            m_reloadTime = m_currentWeapon.WeaponsData.TimeToReload;
            m_isReloading = false;
            m_currentWeapon.Reload();

           
        }
        
        public void SetPickedWeapon(Weapon weapon) {
            m_currentWeapon = weapon;
            m_fireCooldown = m_currentWeapon.WeaponsData.FireCooldown;
            m_reloadTime = m_currentWeapon.WeaponsData.TimeToReload;
            m_currentWeapon.Reload();
            m_aimAction = m_currentWeapon.GetComponent<AimAction>();
        }

        public AimAction GetAimAction() { 
            return m_aimAction;
        }
        
        private void Shoot() {
            if (m_fireCooldown > 0 || m_currentWeapon == null || !m_isFiring || m_isReloading) return;

            if (!m_currentWeapon.CanShoot()) {
                m_isReloading = true;
                return;
            }
            
            m_currentWeapon.Shoot();
            m_fireCooldown = m_currentWeapon.WeaponsData.FireCooldown;
        }

        private void RepositionWeapon(Transform transformHolder) {
            m_currentWeapon.transform.SetParent(transformHolder);
            m_currentWeapon.transform.DOLocalMove(Vector3.zero, 0.1f);
        }
    }
}
