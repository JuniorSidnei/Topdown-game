using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace topdownGame.Weapons.Info {

    [CreateAssetMenu(menuName = "TopDown/Character/Weapons")]
    public class WeaponsData : ScriptableObject {

        public enum WeaponTypeData {
            None, Pistol, Shotgun
        }

        public WeaponTypeData WeaponType;
        public GameObject WeaponPrefab;
        public GameObject BulletObj;
        public float Damage;
        public float FireCooldown;
    }
}