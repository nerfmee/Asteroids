using System;
using System.Collections.Generic;

namespace Asteroids.Game.Signals
{
    public delegate void SignalCallback<T>(T signal);

    public class SignalService
    {
        private static SignalService _instance;

        private static SignalService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SignalService();

                return _instance;
            }
        }

        private readonly Dictionary<Type, List<Delegate>> observers = new Dictionary<Type, List<Delegate>>();

        public static void Subscribe<T>(SignalCallback<T> callback) where T : Signal, new()
        {
            var localType = typeof(T);

            if (Instance.observers.ContainsKey(localType))
            {
                Instance.observers[localType].Add(callback);
            }
            else
            {
                Instance.observers.Add(localType, new List<Delegate> { callback });
            }
        }

        public static void RemoveSignal<T>(SignalCallback<T> callback) where T : Signal, new()
        {
            if (Instance.observers.ContainsKey(typeof(T)))
            {
                var temp = new List<Delegate>();
                temp.AddRange(Instance.observers[typeof(T)]);

                var toRemove = temp.FindAll(t => t.Target.Equals(callback.Target));
                foreach (var item in toRemove)
                {
                    temp.Remove(item);
                }
                Instance.observers[typeof(T)] = temp;
            }
        }

        public static void Publish<T>(T signalObject = null) where T : Signal, new()
        {
            if (signalObject == null)
            {
                signalObject = new T();
            }

            if (Instance.observers.TryGetValue(typeof(T), out var callbacks))
            {
                foreach (var item in callbacks)
                {
                    item?.DynamicInvoke(signalObject);
                }
            }
        }
    }
}