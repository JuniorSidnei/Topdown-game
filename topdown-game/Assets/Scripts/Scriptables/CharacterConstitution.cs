using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace topdownGame.Characters.Status {

    [CreateAssetMenu(menuName = "TopDown/CharacterStatus")]
    public class CharacterConstitution : ScriptableObject, ISerializationCallbackReceiver {

        public float MaxHealth;
        public float CurrentHealth;
        public float MaxStamina;
        public float CurrentStamina;

        private float m_currentHealth;
        private float m_currentStamina;
        
        //alteram valor durante o jogo
        private float m_currentHealthInGame; 
        private float m_currentStaminaInGame; 
            
        public void OnBeforeSerialize() {
            CurrentHealth = MaxHealth;
            CurrentStamina = MaxStamina;
        }

        public void OnAfterDeserialize() {
            CurrentHealth = MaxHealth;
            CurrentStamina = MaxStamina;
        }

        public float MaxHealthInGame {
            get => MaxHealth;
            set => MaxHealth = value;
        }
        
        public float CurrentHealthInGame {
            get => CurrentHealth;
            set => CurrentHealth = value;
        }
        
        public float MaxStaminaInGame {
            get => MaxStamina;
            set => MaxStamina = value;
        }
        
        public float CurrentStaminaInGame {
            get => CurrentStamina;
            set => CurrentStamina = value;
        }
    }
}
