using MeShineFactory.WhacAMole.Level.Model;

namespace MeShineFactory.WhacAMole.Level.Controller
{
    public interface ILevelView
    {
        void Setup(ILevelModel levelModel);
        void Dispose();
    }
}
