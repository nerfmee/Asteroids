using Asteroids.Game.Signals;

namespace Asteroids.Game.UI
{
    public class MainMenuView : BaseView
    {
        public override void OnScreenEnter()
        {
            base.OnScreenEnter();
            SignalService.Subscribe<SpaceBarPressedSignal>(OnSpaceBarPressed);
        }

        public override void OnScreenExit()
        {
            base.OnScreenExit();
            SignalService.RemoveSignal<SpaceBarPressedSignal>(OnSpaceBarPressed);
        }

        private void OnSpaceBarPressed(SpaceBarPressedSignal signal)
        {
            if (MainManager.CurrentGameState == GameState.Ready)
            {
                SignalService.Publish(new GameStateUpdateSignal { Value = GameState.Running });
            }
        }

        public void StartGameButton()
        {
            SignalService.Publish(new GameStateUpdateSignal { Value = GameState.Running });
        }
    }
}