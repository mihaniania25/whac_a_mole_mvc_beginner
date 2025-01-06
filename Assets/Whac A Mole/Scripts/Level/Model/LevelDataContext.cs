using System;

namespace MeShineFactory.WhacAMole.Level.Model
{
    public class LevelDataContext
    {
        public MoleType[] Moles { get; set; }

        public event Action<int, MoleType> OnMoleUpdated;

        public void SetMole(int moleIndex, MoleType moleType)
        {
            if (Moles[moleIndex] != moleType)
            {
                Moles[moleIndex] = moleType;
                OnMoleUpdated?.Invoke(moleIndex, moleType);
            }
        }
    }
}
