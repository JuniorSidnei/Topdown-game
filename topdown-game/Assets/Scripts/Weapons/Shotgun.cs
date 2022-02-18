using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace topdownGame.Weapons.Shotgum {

    public class Shotgun : Weapon {
        public List<Transform> SpawnsTransforns;
        
        public override void Shoot() {
            foreach (var spawn in SpawnsTransforns) {
                var tempBullet = Instantiate(WeaponsData.BulletObj, spawn.position, Quaternion.identity);
                tempBullet.transform.right = spawn.right;
            }
        }

        public override void Especial()
        {
            throw new System.NotImplementedException();
        }
    }
}