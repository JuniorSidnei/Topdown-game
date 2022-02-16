using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace topdownGame.Weapons.Info {

    [CreateAssetMenu(menuName = "TopDown/Character/Weapons")]
    public class WeaponsData : ScriptableObject {
        
        public GameObject BulletObj;
        public float Damage;
        public float FireCooldown;
    }
}