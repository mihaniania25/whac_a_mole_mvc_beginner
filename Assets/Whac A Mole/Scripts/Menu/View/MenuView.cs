using System;
using UnityEngine;
using UnityEngine.UI;
using MeShineFactory.WhacAMole.Menu.Controller;
using MeShineFactory.WhacAMole.Menu.Model;

namespace MeShineFactory.WhacAMole.Menu.View
{
    public class MenuView : MonoBehaviour, IMenuView
    {
        [SerializeField] private Button startButton;

        private IMenuModel model;

        public event Action OnStartInvoked;

        public void Setup(IMenuModel model)
        {
            this.model = model;
            startButton.onClick.AddListener(OnStartButtonClick);
        }

        private void OnStartButtonClick()
        {
            OnStartInvoked?.Invoke();
        }

        public void Dispose()
        {
            model = null;
            startButton.onClick.RemoveListener(OnStartButtonClick);
        }
    }
}
