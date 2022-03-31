using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using topdownGame.Actions;
using topdownGame.Characters.Status;
using topdownGame.Events;
using topdownGame.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace topdownGame.Enemy.Actions {
    public class EnemyConstitution : MonoBehaviour {
        [SerializeField] private CharacterConstitution m_characterConstitution;
        private Character m_character;

        public Image LifeRend;

        private float m_life;
        
        private void Awake() {
            m_character = GetComponent<Character>();
            GameManager.Instance.GlobalDispatcher.Subscribe<OnLifeUpdate>(OnLifeUpdate);
            m_life = m_characterConstitution.MaxHealth;
        }

        private void OnLifeUpdate(OnLifeUpdate ev) {
            if (ev.Character != m_character || m_life <= 0) return;
            
            m_life -= ev.Amount;
            
            if (m_life <= 0) {
                GameManager.Instance.GlobalDispatcher.Emit(new OnEnemySeekerDeath(gameObject));
                return;
            }
            
            LifeRend.DOFillAmount(m_life / m_characterConstitution.MaxHealth, 0.15f);
        }
    }
}