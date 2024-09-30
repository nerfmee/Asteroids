using Asteroids.Game.Management;
using Asteroids.Game.Services;
using Asteroids.Game.Signals;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Asteroids.Game.UI
{
    public class GameplayView : BaseView
    {
        [SerializeField] private TextMeshProUGUI scoreLabel;
        [SerializeField] private TextMeshProUGUI titleLabel;
        [SerializeField] private GameObject stubChances;
        [SerializeField] private Image chanceImage;
        [SerializeField] private Image[] totalChances;
        
        public override void OnScreenEnter()
        {
            base.OnScreenEnter();
            _signalService.Subscribe<DisplayScoreSignal>(OnScoreUpdated);
            _signalService.Subscribe<UpdatePlayerLivesSignal>(OnPlayerLifeChanged);
        }

        public override void OnScreenExit()
        {
            base.OnScreenExit();

            _signalService.RemoveSignal<DisplayScoreSignal>(OnScoreUpdated);
            _signalService.RemoveSignal<UpdatePlayerLivesSignal>(OnPlayerLifeChanged);

            Clear();
        }

        public void DisplayPlayerLivesUI(int count)
        {
            Clear();

            totalChances = new Image[count];
            for (int i = 0; i < totalChances.Length; i++)
            {
                totalChances[i] = Instantiate(chanceImage, stubChances.transform);
                totalChances[i].gameObject.SetActive(true);
            }
        }

        public void DisplayScore(int currentScore)
        {
            scoreLabel.text = $"Score :{currentScore}";
        }

        public void SetTitle(string title)
        {
            titleLabel.text = title;
        }

        public void Clear()
        {
            if (totalChances != null)
            {
                for (int i = 0; i < totalChances.Length; i++)
                {
                    Destroy(totalChances[i].gameObject);
                }
                totalChances = null;
            }
        }

        private void OnScoreUpdated(DisplayScoreSignal signal) => DisplayScore(signal.Value);

        private void OnPlayerLifeChanged(UpdatePlayerLivesSignal signal)
        {
            for (int i = totalChances.Length - 1; i >= signal.Value; i--)
            {
                totalChances[i].gameObject.SetActive(false);
            }
        }

        internal void DelayedStartNewGame()
        {
            StartCoroutine(DelayedCall(2f));
        }

        private IEnumerator DelayedCall(float delay)
        {
            yield return new WaitForSeconds(delay);

            _signalService.Publish(new GameStateUpdateSignal { Value = GameState.Ready });
        }
    }
}