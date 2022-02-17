using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

        private GameObject m_currentPlayer;

        private void Start() {
            GameManager.Instance.GlobalDispatcher.Subscribe<OnPickUpItem>(OnPickUpItem);
            WeaponName.text = WeaponData.name;
        }
        
        private void OnPickUpItem(OnPickUpItem ev) {
            if (!m_currentPlayer) return;

            GameManager.Instance.GlobalDispatcher.Emit(new OnPickedWeapon(WeaponData.WeaponPrefab));
            Destroy(gameObject);
        }
        
        private void OnTriggerEnter2D(Collider2D other) {
            if(((1 << other.gameObject.layer) & PlayerLayer) == 0) {
                return;
            }

            m_currentPlayer = other.gameObject;
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