using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace topdownGame.Characters.Status {

    [CreateAssetMenu(menuName = "TopDown/CharacterStatus")]
    public class CharacterConstitution : ScriptableObject, ISerializationCallbackReceiver {

        public float MaxHealth;
        public float MaxStamina;

        //alteram valor durante o jogo
        private float m_currentHealthInGame; 
        private float m_currentStaminaInGame; 
            
        public void OnBeforeSerialize() {
            m_currentHealthInGame = MaxHealth;
            m_currentStaminaInGame = MaxStamina;
        }

        public void OnAfterDeserialize() {
            m_currentHealthInGame = MaxHealth;
            m_currentStaminaInGame = MaxStamina;
        }

        public float MaxHealthInGame {
            get => MaxHealth;
            set => MaxHealth = value;
        }
        
        public float CurrentHealthInGame {
            get => m_currentHealthInGame;
            set => m_currentHealthInGame = value;
        }
        
        public float MaxStaminaInGame {
            get => MaxStamina;
            set => MaxStamina = value;
        }
        
        public float CurrentStaminaInGame {
            get => m_currentStaminaInGame;
            set => m_currentStaminaInGame = value;
        }
    }
}
