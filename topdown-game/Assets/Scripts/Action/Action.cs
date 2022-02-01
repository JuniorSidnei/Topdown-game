using System.Collections;
using System.Collections.Generic;
using topdownGame.Player;
using UnityEngine;
using UnityEngine.AI;

namespace topdownGame.Actions  {

    public interface IAction {
        public void Configure(Character character);
        public void Activate();
        public void Deactivate();

        public string GetActionName();
    }

    public abstract class Action : IAction {
        
        protected Character Character;
        public bool IsActionActive;
        protected string m_name;
        
        public void Configure(Character character) {
            Character = character;
            m_name = GetType().Name;
            OnConfigure();
        }

        public void Activate()  {
            IsActionActive = true;
            OnActivate();
        }

        public void Deactivate() {
            IsActionActive = false;
            OnDeactivate();
        }

        public string GetActionName() {
            return m_name;
        }

        public abstract void OnConfigure();
        public abstract void OnActivate();
        public abstract void OnDeactivate();
    }
}