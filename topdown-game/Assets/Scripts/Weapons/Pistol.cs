using System;
using System.Collections;
using System.Collections.Generic;
using topdownGame.Weapons.Info;
using UnityEngine;

namespace topdownGame.Weapons.Pistol {
    
    [Serializable]
    public class Pistol : Weapon {
        
        public Transform SpawnTransform;
        
        public override void Shoot() {
            var tempBullet = Instantiate(WeaponsData.BulletObj, SpawnTransform.position, Quaternion.identity);
            tempBullet.transform.right = transform.right;
        }

        public override void Especial()
        {
            Debug.Log("especial de pistola");
        }
    }
}