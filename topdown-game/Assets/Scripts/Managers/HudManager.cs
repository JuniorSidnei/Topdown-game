using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using topdownGame.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace topdownGame.Hud {

    public class HudManager : Singleton<HudManager> {

        public Image LifeBar;
        public Image StaminaBar;

        [Header("pause settings")]
        public GameObject PausePanel;
        public Button RetryButton;
        public Button QuitButton;
        
        private void Awake()
        {
            RetryButton.onClick.AddListener(() => {
                Time.timeScale = 1;
                SceneManager.LoadSceneAsync("sand_box_scene");
            });
            
            QuitButton.onClick.AddListener(Application.Quit);
        }

        public void UpdateLifeBar(float currentLifeInGame) {
            LifeBar.DOFillAmount(currentLifeInGame, 0.2f);
        }
        
        public void UpdateStaminaBar(float currentStaminaInGame) {
            StaminaBar.DOColor(currentStaminaInGame <= 0.1f ? Color.red : Color.white, 0.2f);
            StaminaBar.DOFillAmount(currentStaminaInGame, 0.2f);
        }

        public void ShowPausePanel() {
            PausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}