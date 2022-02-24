using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace topdownGame.Utils.Texts.Damage {
    
    public class DamageText : MonoBehaviour {
        
        public float TextSpeedOffset;

        private void Start() {
            Sequence damageText = DOTween.Sequence();
            damageText.Join(transform.DOMoveY(TextSpeedOffset, 0.5f).SetRelative(true));
            damageText.Join(transform.DOScale(new Vector3(0, 0, 0), 1f));
            damageText.OnComplete(() => {
                
                Destroy(gameObject);
            });
            damageText.Play();
        }
        
    }
}