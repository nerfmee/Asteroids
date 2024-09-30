using Asteroids.Game.Signals;
using Asteroids.Game.UI;

namespace Asteroids.Game.Core
{
    public class GameOverState : BaseGameState
    {
        public override void Execute()
        {
            base.Execute();

            _signalService.Publish<RemoveAllGameEntitiesSignal>();

            MenuManager.HideMenu<MainMenuView>();
            var menu = MenuManager.GetMenu<GameplayView>();
            menu.DisplayScore(_playerProfileService.GetScore());
            menu.SetTitle("Gameover");
            menu.Clear();

            menu.DelayedStartNewGame();
        }
    }
}