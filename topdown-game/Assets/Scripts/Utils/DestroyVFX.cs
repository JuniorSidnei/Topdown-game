﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace topdownGame.Utils {

    public class DestroyVFX : MonoBehaviour {

        private void Start() {
            
            Destroy(gameObject,
                GetComponent<ParticleSystem>().main.duration +
                GetComponent<ParticleSystem>().main.startLifetime.constantMax);
        }
    }
}