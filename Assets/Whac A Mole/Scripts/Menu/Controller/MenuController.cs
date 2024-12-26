using UnityEngine.SceneManagement;
using MeShineFactory.WhacAMole.Menu.Model;
using MeShineFactory.WhacAMole.Utility;

namespace MeShineFactory.WhacAMole.Menu.Controller
{
    public class MenuController : IMenuController
    {
        private IMenuModel model;
        private IMenuView view;

        public void Setup(IMenuView view, IMenuModel model)
        {
            this.view = view;
            this.model = model;

            view.OnStartInvoked += OnStartButtonClick;
        }

        private void OnStartButtonClick()
        {
            SceneManager.LoadScene((int)SceneID.Level);
        }

        public void Dispose()
        {
            if (view != null)
                view.OnStartInvoked -= OnStartButtonClick;
        }
    }
}
