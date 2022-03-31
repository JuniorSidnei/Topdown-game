using System.Collections;
using System.Collections.Generic;
using topdownGame.Events;
using UnityEngine;

namespace topdownGame.Interfaces {


    public interface IDamageable {
        
        void Damage(OnBulletHit.EmitterInfo emitter, OnBulletHit.ReceiverInfo receiver, bool destroyEmitterImmediately = false);
    }
}