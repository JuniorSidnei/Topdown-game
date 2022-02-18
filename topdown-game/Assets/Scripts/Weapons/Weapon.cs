using System.Collections;
using System.Collections.Generic;
using topdownGame.Weapons.Info;
using UnityEngine;

namespace topdownGame.Weapons {
    public abstract class Weapon : MonoBehaviour {
        
        public WeaponsData WeaponsData;

        //public abstract WeaponsData GetData();
        public abstract void Shoot();
        public abstract void Especial();
        public abstract void Reload();
        public abstract bool CanShoot();
    }
}