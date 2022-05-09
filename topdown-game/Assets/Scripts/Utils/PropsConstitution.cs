using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using topdownGame.Events;
using topdownGame.Interfaces;
using topdownGame.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace topdownGame.Utils.Props {

    public class PropsConstitution : MonoBehaviour, IDamageable {
        public int Life;
        public GameObject DestroyedProp;
        public GameObject PropParts;

        public void Damage(OnBulletHit.EmitterInfo emitter, OnBulletHit.ReceiverInfo receiver, bool destroyEmitterImmediately = false) {
            Life -= emitter.Damage;
            GameManager.Instance.GlobalDispatcher.Emit(new OnCameraScreenShake(1.5f, 0.1f));
            if (!(Life <= 0)) return;
            
            Instantiate(PropParts, transform.position, Quaternion.identity, transform.parent);
            Instantiate(DestroyedProp, transform.position, Quaternion.identity, transform.parent);
            Destroy(gameObject);
        }
    }
}