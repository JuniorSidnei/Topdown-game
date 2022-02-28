using System.Collections;
using System.Collections.Generic;
using topdownGame.Actions;
using UnityEngine;

namespace topdownGame.Characters.Animation
{
    public class CharacterAnimation : MonoBehaviour {
        [SerializeField] private Character m_character;
        private AimAction m_aimAction;

        private Animator m_anim;
        private Vector2 m_charVelocity;

        private void Start() {
            m_anim = GetComponent<Animator>();
            m_aimAction = m_character.GetComponent<AimAction>();
        }
        
        private void Update()  {
            m_anim.SetFloat("deltaMovement", m_character.PositionDelta.sqrMagnitude);

            //var aimAction = m_shootAction.GetAimAction();
            //if (aimAction) {
                m_anim.SetFloat("deltaX", m_aimAction.AimDirection.normalized.x);
                m_anim.SetFloat("deltaY", m_aimAction.AimDirection.normalized.y);
            //}
        }
    }
}