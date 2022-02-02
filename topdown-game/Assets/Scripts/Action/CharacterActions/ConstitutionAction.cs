using System.Collections;
using System.Collections.Generic;
using topdownGame.Characters.Status;
using topdownGame.Events;
using topdownGame.Managers;
using UnityEngine;

namespace topdownGame.Actions {
    
    public class ConstitutionAction : MonoBehaviour {

        [SerializeField] private CharacterConstitution m_characterConstitution;
        
        private void Start() {
            GameManager.Instance.GlobalDispatcher.Subscribe<OnStaminaUpdate>(OnStaminaUpdate);
        }

        private void OnStaminaUpdate(OnStaminaUpdate ev) {
            m_characterConstitution.CurrentStaminaInGame -= ev.Amount;
            //TODO update ui stamina bar
        }
    }
}