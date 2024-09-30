namespace Asteroids.Game.Signals
{
    public delegate void SignalCallback<T>(T signal);

    public interface ISignalService
    {
        public void Subscribe<T>(SignalCallback<T> callback) where T : Signal, new();

        public void RemoveSignal<T>(SignalCallback<T> callback) where T : Signal, new();

        public void Publish<T>(T signalObject = default) where T : Signal, new();
    }
}