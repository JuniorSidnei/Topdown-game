using System.Collections;
using System.Collections.Generic;
using topdownGame.Actions;
using topdownGame.Characters;
using UnityEngine;

namespace topdownGame.Characters  {
    public class EnemyCharacterAnimation : CharacterAnimation  {
       
        private AimToTarget m_aimToTarget;

        private void Start() {
            m_aimToTarget = Character.GetComponent<AimToTarget>();  
        }
        
        private void Update()  {
            Animator.SetFloat("deltaX", m_aimToTarget.AimDirection.normalized.x);
            Animator.SetFloat("deltaY", m_aimToTarget.AimDirection.normalized.y);
        }
        
    }
}