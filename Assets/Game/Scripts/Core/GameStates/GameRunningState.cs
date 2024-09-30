using Asteroids.Game.UI;

namespace Asteroids.Game.Core
{
    public class GameRunningState : BaseGameState
    {
        public override void Execute()
        {
            base.Execute();

            _playerProfileService.SetScore(0);
            _playerProfileService.SetTotalLives(GameConfig.TotalLives);

            MenuManager.HideMenu<MainMenuView>();
            var menu = MenuManager.ShowMenu<GameplayView>();
            menu.DisplayScore(_playerProfileService.GetScore());
            menu.SetTitle(string.Empty);
            menu.DisplayPlayerLivesUI(GameConfig.TotalLives);
        }
    }
}