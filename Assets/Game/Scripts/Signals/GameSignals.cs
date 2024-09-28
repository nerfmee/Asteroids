using Asteroids.Game.Config;
using Asteroids.Game.Management;

namespace Asteroids.Game.Signals
{
    public class UpdateScoreSignal : Signal<int>
    {
    }

    public class DisplayScoreSignal : Signal<int>
    {
    }

    public class SpaceBarPressedSignal : Signal
    {
    }

    public class GameStateUpdateSignal : Signal<GameState>
    {
    }

    public class UpdatePlayerLivesSignal : Signal<int>
    {
    }

    public class PlayerDiedSignal : Signal
    {
    }

    public class PlayerReviveSignal : Signal
    {
    }

    public class GameConfigLoadedSignal : Signal<GameConfig>
    {

    }
}