using MeShineFactory.WhacAMole.Level.Model;

namespace MeShineFactory.WhacAMole.Level.Controller
{
    public class LevelController : ILevelController
    {
        private ILevelView view;
        private ILevelModel levelModel;

        public void Setup(ILevelView view, ILevelModel levelModel)
        {
            this.view = view;
            this.levelModel = levelModel;

            view.OnMoleBeingTouched += levelModel.HitMole;
        }

        public void Dispose()
        {
            view.OnMoleBeingTouched -= levelModel.HitMole;
        }
    }
}
