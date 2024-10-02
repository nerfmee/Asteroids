using Asteroids.Game.Config;
using Asteroids.Game.Management;
using UnityEngine;

namespace Asteroids.Game.Signals
{
    public class UpdateScoreSignal : Signal<int> { }
    public class DisplayScoreSignal : Signal<int> { }
    public class UpdatePlayerPositionSignal : Signal<Vector3> { }
    public class UpdatePlayerRotationSignal : Signal<float> { }
    public class UpdatePlayerSpeedSignal : Signal<float> { }
    public class UpdatePlayerLaserChargesSignal : Signal<int> { }
    public class UpdatePlayerLaserCooldownSignal : Signal<float> { }
    public class UpdatePlayerLivesSignal : Signal<int> { }
    public class RemoveAllGameEntitiesSignal : Signal { }
    public class GameStateUpdateSignal : Signal<GameState> { }
    public class PlayerDiedSignal : Signal { }
    public class PlayerReviveSignal : Signal { }
    public class GameConfigLoadedSignal : Signal<GameConfig> { }
}