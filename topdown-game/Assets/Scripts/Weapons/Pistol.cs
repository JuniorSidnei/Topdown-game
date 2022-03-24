using System;
using System.Collections;
using System.Collections.Generic;
using topdownGame.Bullets;
using topdownGame.Weapons.Info;
using UnityEngine;

namespace topdownGame.Weapons.Pistol {
    
    [Serializable]
    public class Pistol : Weapon {
        
        public Transform SpawnTransform;
        private int m_currentAmmunition;
        
        public WeaponsData GetData() {
            return WeaponsData;
        }

        public override void Shoot(LayerMask ownerLayer) {
            var tempBullet = Instantiate(WeaponsData.BulletObj, SpawnTransform.position, Quaternion.identity);
            tempBullet.GetComponent<SimpleBullet>().SetOwnerLayer(ownerLayer);
            tempBullet.transform.right = transform.right;
            m_currentAmmunition -= 1;
        }

        public override void Especial()
        {
            Debug.Log("especial de pistola");
        }

        public override void Reload() {
            m_currentAmmunition = WeaponsData.AmmunitionAmount;
        }

        public override bool CanShoot() {
            return m_currentAmmunition > 0;
        }

        public override void ActivateTriggerCollider() {
            TriggerCollider.enabled = true;
        }

        public override void DeactivateTriggerCollider() {
            TriggerCollider.enabled = false;
        }
    }
}