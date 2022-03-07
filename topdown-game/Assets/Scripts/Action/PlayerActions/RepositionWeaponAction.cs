using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using topdownGame.Events;
using topdownGame.Managers;
using UnityEngine;

namespace topdownGame.Actions
{


    public class RepositionWeaponAction : MonoBehaviour
    {
        
        public Vector3 m_rightPosition;
        public Vector3 m_leftPosition;
        public Transform WeaponTarget;
        
        private void Awake() {
            GameManager.Instance.GlobalDispatcher.Subscribe<OnMove>(OnMove); 
        }

        private void OnMove(OnMove ev) {
            if (ev.InputDirection.x > 1) {
                transform.DOMove(m_rightPosition, 0.1f);
            } else if (ev.InputDirection.x < -1) {
                transform.DOMove(m_leftPosition, 0.1f);
            }
        }
    }
}