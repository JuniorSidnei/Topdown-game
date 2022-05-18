using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace topdownGame.Weapons.Info {

    [CreateAssetMenu(menuName = "TopDown/Character/Weapons")]
    public class WeaponsData : ScriptableObject {

        public enum WeaponTypeData {
            None, Pistol, Shotgun, Machinegun
        }

        public WeaponTypeData WeaponType;
        public GameObject WeaponPrefab;
        public GameObject BulletObj;
        public int Damage;
        public float FireCooldown;
        public int AmmunitionAmount;
        public int CurrentAmmunition;
        public float TimeToReload;
        public Sprite BulletSprite;
    }
}