using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using topdownGame.Events;
using topdownGame.Managers;
using topdownGame.Pickable.Weapons;
using topdownGame.Weapons;
using topdownGame.Weapons.Info;
using UnityEngine;

namespace topdownGame.Actions.Pickable {

    public class PickupWeaponAction : MonoBehaviour {
        
        public Transform WeaponParent;
        public WeaponsData.WeaponTypeData CurrentWeaponType = WeaponsData.WeaponTypeData.None;

        private Weapon m_currentWeapon;

        public void EquipWeapon(Weapon weapon) {
            if(m_currentWeapon) DropWeapon();
            
            m_currentWeapon = weapon;
            var weaponTransform = m_currentWeapon.transform;
            weaponTransform.SetParent(WeaponParent);
            weaponTransform.DOLocalMove(Vector3.zero, 0.5f);
            weaponTransform.localRotation = Quaternion.identity;
            var aim = m_currentWeapon.GetComponent<AimAction>();
            aim.Show(true);
            CurrentWeaponType = m_currentWeapon.WeaponsData.WeaponType;
        }

        public void DropWeapon() {
            if (!m_currentWeapon) return;
            
            var weaponTransform = m_currentWeapon.transform;
            weaponTransform.SetParent(null);
            weaponTransform.localPosition = transform.position;
            CurrentWeaponType = WeaponsData.WeaponTypeData.None;
            var aim = m_currentWeapon.GetComponent<AimAction>();
            aim.Show(false);
            m_currentWeapon = null;
        }
    }
}