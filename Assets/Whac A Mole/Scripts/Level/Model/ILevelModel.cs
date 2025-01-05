using System;

namespace MeShineFactory.WhacAMole.Level.Model
{
    public interface ILevelModel
    {
        int HolesNumber { get; }
        MoleType[] Moles { get; }

        event Action<int, MoleType> OnMoleUpdated;

        void Setup(LevelConfiguration configuration);
        void Dispose();

        void HitMole(int moleIndex);
    }
}
