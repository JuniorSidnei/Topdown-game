using System;
using System.Collections;
using System.Collections.Generic;
using topdownGame.Actions;
using UnityEngine;

namespace topdownGame.Characters  {
    public class CharacterAnimation : MonoBehaviour {
        [SerializeField] private Character m_character;

        private Animator m_anim;

        protected Character Character => m_character;
        protected Animator Animator => m_anim;

        private void Awake() {
            m_anim = GetComponent<Animator>();
        }

        private void FixedUpdate() {
            m_anim.SetFloat("deltaMovement", m_character.PositionDelta.sqrMagnitude);
        }
    }
}