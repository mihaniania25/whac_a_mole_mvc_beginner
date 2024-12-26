using MeShineFactory.WhacAMole.Menu.Model;

namespace MeShineFactory.WhacAMole.Menu.Controller
{
    public interface IMenuController
    {
        void Setup(IMenuView view, IMenuModel model);
        void Dispose();
    }
}
