using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Game.UI
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private List<BaseView> views;

        private static MenuManager _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }

        public static T ShowMenu<T>() where T : BaseView
        {
            var menu = GetMenu<T>();
            menu.SetVisibility(true);
            menu.OnScreenEnter();
            return menu;
        }

        public static T GetMenu<T>() where T : BaseView
        {
            var menu = _instance.views.Find(t => t.GetType().Equals(typeof(T)));
            return menu as T;
        }

        public static void HideMenu<T>() where T : BaseView
        {
            var menu = GetMenu<T>();
            menu.OnScreenExit();
            menu.SetVisibility(false);
        }
    }
}