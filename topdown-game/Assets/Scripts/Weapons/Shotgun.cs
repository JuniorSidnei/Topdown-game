using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace topdownGame.Weapons.Shotgum
{


    public class Shotgun : Weapon
    {
        public Transform AimTransform;
        public List<Transform> SpawnsTransforns;
        
        public override void Shoot()
        {
            foreach (var spawn in SpawnsTransforns) {
                Debug.Log("tiro de 12, spawn: " + spawn.name);
            }

        }

        public override void Especial()
        {
            throw new System.NotImplementedException();
        }
    }
}