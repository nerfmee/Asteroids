using System.Collections;
using UnityEngine;

namespace Asteroids.Game.Core
{
    public class CoroutineRunner : MonoBehaviour
    {
        private static CoroutineRunner _instance;

        
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            
            DontDestroyOnLoad(gameObject);
        }
        
        public static CoroutineRunner Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("CoroutineRunner is not present in the scene. Please add it.");
                }
                return _instance;
            }
        }

        public Coroutine StartCoroutine(IEnumerator coroutine)
        {
            return base.StartCoroutine(coroutine);
        }

        public void StopCoroutine(Coroutine coroutine)
        {
            base.StopCoroutine(coroutine);
        }

        public void StopCoroutine(string methodName)
        {
            base.StopCoroutine(methodName);
        }
    }
}