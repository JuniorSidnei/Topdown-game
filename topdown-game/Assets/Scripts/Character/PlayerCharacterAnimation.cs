using System.Collections;
using System.Collections.Generic;
using topdownGame.Actions;
using topdownGame.Characters;
using UnityEngine;

public class PlayerCharacterAnimation : CharacterAnimation {

    private AimAction m_aimAction;
    
    private void Start()  {
        m_aimAction = Character.GetComponent<AimAction>();   
    }

    
    private void Update()  {
        Animator.SetFloat("deltaX", m_aimAction.AimDirection.normalized.x);
        Animator.SetFloat("deltaY", m_aimAction.AimDirection.normalized.y);
    }
}
