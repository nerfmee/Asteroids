using Asteroids.Game.UI;

namespace Asteroids.Game.Core
{
    public class GameReadyState : BaseGameState
    {
        public override void Execute()
        {
            base.Execute();

            var mainMenu = MenuManager.ShowMenu<MainMenuView>();
            MenuManager.HideMenu<GameplayView>();
            mainMenu.ToggleStartButton(true);
        }
    }
}