using System;
using MeShineFactory.WhacAMole.Menu.Model;

namespace MeShineFactory.WhacAMole.Menu.Controller
{
    public interface IMenuView
    {
        event Action OnStartInvoked;

        void Setup(IMenuModel model);
        void Dispose();
    }
}