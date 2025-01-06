using System;

namespace MeShineFactory.WhacAMole.Level.Model
{
    public class LevelModel : ILevelModel
    {
        private const int HOLES_NUMBER = 9;
        
        private LevelConfiguration configuration;

        private LevelDataContext dataContext;
        private LevelMolesGenerator molesGenerator;

        public int HolesNumber => HOLES_NUMBER;

        public MoleType[] Moles
        {
            get => dataContext.Moles;
            private set => dataContext.Moles = value;
        }

        public event Action<int, MoleType> OnMoleUpdated;

        public void Setup(LevelConfiguration configuration)
        {
            dataContext = new();
            molesGenerator = new();
            this.configuration = configuration;
            Moles = new MoleType[HolesNumber];

            dataContext.OnMoleUpdated += OnMolesDataUpdated;

            molesGenerator.Setup(configuration, dataContext);
        }

        private void OnMolesDataUpdated(int moleIndex, MoleType moleType)
        {
            OnMoleUpdated?.Invoke(moleIndex, moleType);
        }

        public void HitMole(int moleIndex)
        {
            molesGenerator.TryFreeTheMole(moleIndex);
        }

        public void Dispose()
        {
            molesGenerator.Dispose();
            dataContext.OnMoleUpdated -= OnMolesDataUpdated;
        }
    }
}
