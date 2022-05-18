using System.Collections;
using System.Collections.Generic;
using topdownGame.Weapons.Info;
using UnityEngine;

namespace topdownGame.Weapons {
    public abstract class Weapon : MonoBehaviour {

        public CircleCollider2D TriggerCollider;
        public WeaponsData WeaponsData;

        //[HideInInspector]
        public Transform InitialParent;
        //[HideInInspector]
        public Vector3 InitialPosition;
        public bool IsShowCaseWeapon;
        
        public abstract void Shoot(LayerMask ownerLayer);
        public abstract void Especial();
        public abstract void Reload();
        public abstract bool CanShoot();
        public abstract void ActivateTriggerCollider();
        public abstract void DeactivateTriggerCollider();
        public abstract void Unequip();
    }
}