using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeShineFactory.WhacAMole.Utility;

namespace MeShineFactory.WhacAMole.Level.Model
{
    public class LevelModel : ILevelModel
    {
        private const int HOLES_NUMBER = 9;

        private bool isPlaying = false;
        private CoroutineTask levelFlowTask;
        private Dictionary<int, CoroutineTask> despawnMoleTasks = new();
        private LevelConfiguration configuration;

        public int HolesNumber => HOLES_NUMBER;
        public MoleType[] Moles { get; private set; }

        public event Action<int, MoleType> OnMoleUpdated;

        public void Setup(LevelConfiguration configuration)
        {
            this.configuration = configuration;
            Moles = new MoleType[HolesNumber];

            isPlaying = true;
            levelFlowTask = new(LevelFlowRoutine());
        }

        private IEnumerator LevelFlowRoutine()
        {
            Debug.Log($"[LevelModel] level routine started");
            yield return new WaitForSeconds(configuration.GameDelay);

            while (isPlaying)
            {
                TrySpawnMoleInRandomHole();
                yield return new WaitForSeconds(configuration.SpawnInterval.GetRandomValue());
            }
        }

        private void TrySpawnMoleInRandomHole()
        {
            List<int> freeHolesIndexes = GetFreeHolesIndexes();

            if (freeHolesIndexes.Count > 0)
            {
                MoleType moleType = GetMoleType();
                int randomIndex = UnityEngine.Random.Range(0, freeHolesIndexes.Count);

                Moles[randomIndex] = moleType;
                OnMoleUpdated?.Invoke(randomIndex, moleType);

                despawnMoleTasks.Add(randomIndex, new(DespawnMole(randomIndex)));

                Debug.Log($"[LevelModel] mole spawned");
            }
        }

        private MoleType GetMoleType()
        {
            return MoleType.Turtle;
        }

        private List<int> GetFreeHolesIndexes()
        {
            List<int> indexes = new();

            for (int i = 0; i < Moles.Length; i++)
            {
                if (Moles[i] is MoleType.None)
                    indexes.Add(i);
            }

            return indexes;
        }

        private IEnumerator DespawnMole(int moleIndex)
        {
            yield return new WaitForSeconds(configuration.DespawnDelay);

            bool moleReleaseSucceed = TryFreeTheMole(moleIndex);
            Debug.Log($"[LevelModel] despawn mole. Succeed='{moleReleaseSucceed}'");
        }

        public void HitMole(int moleIndex)
        {
            TryFreeTheMole(moleIndex);
        }

        private bool TryFreeTheMole(int moleIndex)
        {
            bool isMoleIndexValid = ValidateMoleIndex(moleIndex);

            if (isMoleIndexValid)
            {
                Moles[moleIndex] = MoleType.None;
                OnMoleUpdated?.Invoke(moleIndex, MoleType.None);

                despawnMoleTasks[moleIndex].Stop();
                despawnMoleTasks.Remove(moleIndex);
            }

            return isMoleIndexValid;
        }

        private bool ValidateMoleIndex(int moleIndex)
        {
            if (moleIndex >= 0 && moleIndex < Moles.Length)
                return true;

            Debug.LogError($"[LevelModel] Mole index validation failed");
            return false;
        }

        public void Dispose()
        {
            levelFlowTask?.Stop();
            isPlaying = false;
        }
    }
}
