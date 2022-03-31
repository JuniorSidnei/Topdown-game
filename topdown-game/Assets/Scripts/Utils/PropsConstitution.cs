using System.Collections;
using System.Collections.Generic;
using topdownGame.Events;
using topdownGame.Interfaces;
using UnityEngine;

namespace topdownGame.Utils.Props {

    public class PropsConstitution : MonoBehaviour, IDamageable {
        public int Life;

        public void Damage(OnBulletHit.EmitterInfo emitter, OnBulletHit.ReceiverInfo receiver, bool destroyEmitterImmediately = false) {
            Life -= emitter.Damage;

            if (!(Life <= 0)) return;
            //TODO::spawn particles, play sound, bla bla bla            
            Destroy(gameObject);
        }
    }
}