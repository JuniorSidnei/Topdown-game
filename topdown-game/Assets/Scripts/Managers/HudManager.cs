using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using topdownGame.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace topdownGame.Hud {

    public class HudManager : Singleton<HudManager> {

        public Image LifeBar;
        public Image StaminaBar;

        public void UpdateLifeBar(float currentLifeInGame) {
            LifeBar.DOFillAmount(currentLifeInGame, 0.2f);
        }
        
        public void UpdateStaminaBar(float currentStaminaInGame) {
            StaminaBar.material.DOColor(currentStaminaInGame <= 0.1f ? Color.red : Color.white, 0.2f);
            StaminaBar.DOFillAmount(currentStaminaInGame, 0.2f);
        }
    }
}