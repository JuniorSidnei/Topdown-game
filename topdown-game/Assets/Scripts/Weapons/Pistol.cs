using System;
using System.Collections;
using System.Collections.Generic;
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

        public override void Shoot() {
            var tempBullet = Instantiate(WeaponsData.BulletObj, SpawnTransform.position, Quaternion.identity);
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
    }
}