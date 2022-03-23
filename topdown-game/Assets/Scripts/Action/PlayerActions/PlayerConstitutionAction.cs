using System;
using System.Collections;
using System.Collections.Generic;
using topdownGame.Characters.Status;
using topdownGame.Events;
using topdownGame.Hud;
using topdownGame.Managers;
using UnityEngine;

namespace topdownGame.Actions
{
    public class PlayerConstitutionAction : MonoBehaviour
    {
        public CharacterConstitution CharacterConstitution;
        
        private Character m_character;
        private float m_life;
        
        private void Awake() {
            m_character = GetComponent<Character>();
            m_life = CharacterConstitution.CurrentHealthInGame;
            GameManager.Instance.GlobalDispatcher.Subscribe<OnLifeUpdate>(OnLifeUpdate);    
        }

        private void OnLifeUpdate(OnLifeUpdate ev)
        {
            if (ev.Character != m_character || m_life <= 0) return;
            
            m_life -= ev.Amount;
            
            if (m_life <= 0) {
                //DestroyImmediate(gameObject);
                Debug.Log("end game");
                return;
            }
            
            HudManager.Instance.UpdateLifeBar(m_life/CharacterConstitution.MaxHealthInGame);
        }
        
    }
}