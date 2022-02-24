using System.Collections;
using System.Collections.Generic;
using TMPro;
using topdownGame.Events;
using topdownGame.Managers;
using topdownGame.Utils.Texts.Damage;
using UnityEngine;

namespace topdownGame.Utils.Canvas {
    
    public class CanvasBattle : MonoBehaviour {
        public TextMeshProUGUI DamageText;
        
        private void Start() {
            GameManager.Instance.GlobalDispatcher.Subscribe<OnDamageText>(OnDamageText);
        }


        private void OnDamageText(OnDamageText ev) {
            var damage = Instantiate(DamageText, ev.Position, Quaternion.identity, transform);
            damage.text = ev.Damage.ToString();
        }
    }
}