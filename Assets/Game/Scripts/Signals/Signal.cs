namespace Asteroids.Game.Signals
{
    public class Signal
    {
    }

    public class Signal<T> : Signal
    {
        public T Value;
    }
}