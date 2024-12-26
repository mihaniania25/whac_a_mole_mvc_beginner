using UnityEngine;
using MeShineFactory.WhacAMole.Menu.Controller;
using MeShineFactory.WhacAMole.Menu.Model;
using MeShineFactory.WhacAMole.Menu.View;

namespace MeShineFactory.WhacAMole.Menu
{
    public class MenuLauncher : MonoBehaviour
    {
        [SerializeField] private MenuView view;

        private IMenuModel model;
        private IMenuController controller;

        private void Awake()
        {
            model = GetMenuModel();
            
            view.Setup(model);

            controller = GetMenuController();
            controller.Setup(view, model);
        }

        private IMenuModel GetMenuModel()
        {
            return new MenuModel();
        }

        private IMenuController GetMenuController()
        {
            return new MenuController();
        }

        private void OnDestroy()
        {
            controller.Dispose();
            view.Dispose();

            controller = null;
            model = null;
        }
    }
}
