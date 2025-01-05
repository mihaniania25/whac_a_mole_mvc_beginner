using MeShineFactory.WhacAMole.Level.Model;
using System;

namespace MeShineFactory.WhacAMole.Level.Controller
{
    public interface ILevelView
    {
        event Action<int> OnMoleBeingTouched;

        void Setup(ILevelModel levelModel);
        void Dispose();
    }
}
