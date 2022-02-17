using System;
using System.Collections;
using System.Collections.Generic;
using topdownGame.Events;
using topdownGame.Managers;
using topdownGame.Weapons;
using topdownGame.Weapons.Info;
using UnityEngine;

namespace topdownGame.Actions.Pickable {

    public class PickupWeaponAction : MonoBehaviour {
        
        public Transform WeaponParent;
        public WeaponsData.WeaponTypeData CurrentWeaponType = WeaponsData.WeaponTypeData.None;

        private GameObject m_currentWeapon;

        private void Awake() {
            GameManager.Instance.GlobalDispatcher.Subscribe<OnPickedWeapon>(OnPickedWeapon);
        }

        private void OnPickedWeapon(OnPickedWeapon ev) {
            if (m_currentWeapon) {
                DestroyImmediate(m_currentWeapon);
            }

            var newWeapon = Instantiate(ev.Weapon);
            m_currentWeapon = newWeapon;
            m_currentWeapon.transform.SetParent(WeaponParent);
            m_currentWeapon.transform.localPosition = Vector3.zero;
            m_currentWeapon.transform.localRotation = Quaternion.identity;
            CurrentWeaponType = m_currentWeapon.GetComponent<Weapon>().WeaponsData.WeaponType;
        }
    }
}