using Asteroids.Game.Signals;
using UnityEngine;
using Zenject;

namespace Asteroids.Game.UI
{
    public class BaseView : MonoBehaviour
    {
        protected ISignalService _signalService;

        [Inject]
        private void InitSignalService(ISignalService signalService)
        {
            _signalService = signalService;
        }

        public virtual void OnScreenEnter() { }

        public virtual void OnScreenExit() { }

        public void SetVisibility(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}