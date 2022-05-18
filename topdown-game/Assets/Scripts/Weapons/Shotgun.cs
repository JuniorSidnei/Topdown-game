using System;
using System.Collections;
using System.Collections.Generic;
using topdownGame.Bullets;
using topdownGame.Weapons.Info;
using UnityEngine;

namespace topdownGame.Weapons.Shotgum {

    public class Shotgun : Weapon {
        public List<Transform> SpawnsTransforns;
        
        private void Awake() {
            if (!IsShowCaseWeapon) return;
            
            InitialPosition = transform.localPosition;
        }

        public override void Shoot(LayerMask ownerLayer) {
            foreach (var spawn in SpawnsTransforns) {
                var tempBullet = Instantiate(WeaponsData.BulletObj, spawn.position, Quaternion.identity);
                tempBullet.GetComponent<SimpleBullet>().SetOwnerLayer(ownerLayer);
                tempBullet.transform.right = spawn.right;
            }

            WeaponsData.CurrentAmmunition -= 1;
        }

        public override void Especial()
        {
            throw new System.NotImplementedException();
        }

        public override void Reload() {
            WeaponsData.CurrentAmmunition = WeaponsData.AmmunitionAmount;
        }

        public override bool CanShoot() {
            return WeaponsData.CurrentAmmunition > 0;
        }
        
        public override void ActivateTriggerCollider() {
            TriggerCollider.enabled = true;
        }

        public override void DeactivateTriggerCollider() {
            TriggerCollider.enabled = false;
        }

        public override void Unequip() {
            transform.SetParent(InitialParent);
            transform.localPosition = InitialPosition;
            transform.localRotation = Quaternion.Euler(0,0,0);
        }
    }
}