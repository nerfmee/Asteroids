using Asteroids.Game.Management;
using Asteroids.Game.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids.Game.UI
{
    public class MainMenuView : BaseView
    {
        [SerializeField] private Button buttonStartGame;
        [SerializeField] private TextMeshProUGUI loadingText;

        public override void OnScreenEnter()
        {
            base.OnScreenEnter();
            _signalService.Subscribe<SpaceBarPressedSignal>(OnSpaceBarPressed);

            buttonStartGame.onClick.RemoveAllListeners();
            buttonStartGame.onClick.AddListener(() => StartGameClicked());
        }

        public override void OnScreenExit()
        {
            base.OnScreenExit();
            _signalService.RemoveSignal<SpaceBarPressedSignal>(OnSpaceBarPressed);
        }

        private void OnSpaceBarPressed(SpaceBarPressedSignal signal)
        {
            if (MainManager.CurrentGameState == GameState.Ready)
            {
                StartGameClicked();
            }
        }

        public void StartGameClicked()
        {
            _signalService.Publish(new GameStateUpdateSignal { Value = GameState.Running });
        }

        public void ToggleStartButton(bool isactive)
        {
            buttonStartGame.gameObject.SetActive(isactive);
            loadingText.gameObject.SetActive(!isactive);
        }
    }
}