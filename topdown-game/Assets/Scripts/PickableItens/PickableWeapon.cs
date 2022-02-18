using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using topdownGame.Actions;
using topdownGame.Actions.Pickable;
using topdownGame.Events;
using topdownGame.Managers;
using topdownGame.Weapons;
using topdownGame.Weapons.Info;
using UnityEngine;

namespace topdownGame.Pickable.Weapons  {
    
    public class PickableWeapon : MonoBehaviour {
        
        public LayerMask PlayerLayer;
        public WeaponsData WeaponData;

        [Header("Canvas info")]
        public GameObject WeaponInfo;
        public TextMeshProUGUI WeaponName;

        private Character m_currentPlayer;

        private void Start() {
            Character.OnInteract += OnCharacterInteraction;
            WeaponName.text = WeaponData.name;
        }

        private void OnCharacterInteraction(Character character) {
            if (m_currentPlayer != character) return;

            var weapon = gameObject.GetComponent<Weapon>();
            character.GetComponent<PickupWeaponAction>().EquipWeapon(weapon);
            character.GetComponent<ShootAction>().SetPickedWeapon(weapon);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(((1 << other.gameObject.layer) & PlayerLayer) == 0) {
                return;
            }

            m_currentPlayer = other.gameObject.GetComponent<Character>();
            WeaponInfo.SetActive(true);
        }

        private void OnTriggerExit2D(Collider2D other) {
            if(((1 << other.gameObject.layer) & PlayerLayer) == 0) {
                return;
            }

            m_currentPlayer = null;
            WeaponInfo.SetActive(false);
        }
    }
}