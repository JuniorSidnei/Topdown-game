using System.Collections;
using System.Collections.Generic;
using topdownGame.Bullets;
using topdownGame.Weapons.Info;
using UnityEngine;

namespace topdownGame.Weapons.Shotgum {

    public class Shotgun : Weapon {
        public List<Transform> SpawnsTransforns;
        private int m_currentAmmunition;
        
        public override void Shoot(LayerMask ownerLayer) {
            foreach (var spawn in SpawnsTransforns) {
                var tempBullet = Instantiate(WeaponsData.BulletObj, spawn.position, Quaternion.identity);
                tempBullet.GetComponent<SimpleBullet>().SetOwnerLayer(ownerLayer);
                tempBullet.transform.right = spawn.right;
            }

            m_currentAmmunition -= 1;
        }

        public override void Especial()
        {
            throw new System.NotImplementedException();
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