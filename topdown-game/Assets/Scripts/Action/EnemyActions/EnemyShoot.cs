using System.Collections;
using System.Collections.Generic;
using topdownGame.Actions;
using topdownGame.Events;
using topdownGame.IA;
using topdownGame.Managers;
using topdownGame.Weapons;
using UnityEngine;

namespace topdownGame.Actions
{


    public class EnemyShoot : ShootAction {
        
        [SerializeField] private Weapon m_currentWeapon;
        private AimToTarget m_aimTotarget;

        private Character m_character;
        
        private void Awake() {
            EnemySeeker.OnReached += OnFire;
            m_character = GetComponent<Character>();
            SetPickedWeapon(m_currentWeapon);
        }

        private void OnFire(Character character, bool canShoot) {
            if (character != m_character) return;
            
            m_isFiring = canShoot;
        }

        private void Update() {
            UpdateFireCooldown();
            ShootingAndReloading(m_currentWeapon);
        }
        
        private void SetPickedWeapon(Weapon weapon) {
            m_currentWeapon = weapon;
            m_fireCooldown = m_currentWeapon.WeaponsData.FireCooldown;
            m_reloadTime = m_currentWeapon.WeaponsData.TimeToReload;
            m_currentWeapon.Reload();
        }
    }
}