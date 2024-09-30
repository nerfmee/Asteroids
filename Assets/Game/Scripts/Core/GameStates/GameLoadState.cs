using Asteroids.Game.UI;

namespace Asteroids.Game.Core
{
    public class GameLoadState : BaseGameState
    {
        public override void Execute()
        {
            base.Execute();

            var mainMenu = MenuManager.ShowMenu<MainMenuView>();
            MenuManager.HideMenu<GameplayView>();
            mainMenu.ToggleStartButton(false);
        }
    }
}