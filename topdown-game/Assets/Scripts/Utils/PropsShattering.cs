using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace topdownGame.Utils.Props
{


    public class PropsShattering : MonoBehaviour
    {
        public List<GameObject> Props;
        [Range(0, 3)]
        public float Force;


        private void Start() {
            
            foreach (var prop in Props) {
                prop.transform.DORotate(new Vector3(0, 0, Random.Range(0, 25)), 1f);
                var randPos = new Vector3(transform.position.x + Random.Range(-Force, Force), transform.position.y + Random.Range(-Force, Force), 0);
                prop.transform.DOMove(randPos, 1f);
            }
            Destroy(gameObject, 1.5f);
        }
    }
}