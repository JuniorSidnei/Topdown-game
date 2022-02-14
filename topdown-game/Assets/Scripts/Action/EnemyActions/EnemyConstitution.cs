using System.Collections;
using System.Collections.Generic;
using topdownGame.Actions;
using topdownGame.Characters.Status;
using topdownGame.Events;
using topdownGame.Managers;
using UnityEngine;

namespace topdownGame.Enemy.Actions
{
    public class EnemyConstitution : MonoBehaviour
    {
        [SerializeField] private CharacterConstitution m_characterConstitution;
        private Character m_character;
        
        private void Awake() {
            m_character = GetComponent<Character>();
            GameManager.Instance.GlobalDispatcher.Subscribe<OnLifeUpdate>(OnLifeUpdate);
        }

        private void OnLifeUpdate(OnLifeUpdate ev)
        {
            if (ev.Character != m_character) return;
            
            m_characterConstitution.CurrentHealthInGame -= ev.Amount;
            if (m_characterConstitution.CurrentHealthInGame <= 0) {
                DestroyImmediate(gameObject);
            }
        }
        
    }
}