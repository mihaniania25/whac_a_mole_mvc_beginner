using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MeShineFactory.WhacAMole.Utility;

namespace MeShineFactory.WhacAMole.Level.Model
{
    public class LevelMolesGenerator
    {
        private LevelConfiguration configuration;
        private LevelDataContext dataContext;

        private bool isGenerating = false;
        private CoroutineTask generationTask;
        private Dictionary<int, CoroutineTask> despawnMoleTasks = new();

        public void Setup(LevelConfiguration configuration, LevelDataContext dataContext)
        {
            this.configuration = configuration;
            this.dataContext = dataContext;

            isGenerating = true;
            generationTask = new(GenerationRoutine());
        }

        private IEnumerator GenerationRoutine()
        {
            Debug.Log($"[LevelMolesGenerator] level routine started");
            yield return new WaitForSeconds(configuration.GameDelay);

            while (isGenerating)
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
                int randomIndex = Random.Range(0, freeHolesIndexes.Count);

                dataContext.SetMole(randomIndex, moleType);

                despawnMoleTasks.Add(randomIndex, new(DespawnMole(randomIndex)));

                Debug.Log($"[LevelMolesGenerator] mole spawned");
            }
        }

        private MoleType GetMoleType()
        {
            return MoleType.Turtle;
        }

        private List<int> GetFreeHolesIndexes()
        {
            List<int> indexes = new();

            for (int i = 0; i < dataContext.Moles.Length; i++)
            {
                if (dataContext.Moles[i] is MoleType.None)
                    indexes.Add(i);
            }

            return indexes;
        }

        private IEnumerator DespawnMole(int moleIndex)
        {
            yield return new WaitForSeconds(configuration.DespawnDelay);

            bool moleReleaseSucceed = TryFreeTheMole(moleIndex);
            Debug.Log($"[LevelMolesGenerator] despawn mole. Succeed='{moleReleaseSucceed}'");
        }

        public bool TryFreeTheMole(int moleIndex)
        {
            bool isMoleIndexValid = ValidateMoleIndex(moleIndex);

            if (isMoleIndexValid)
            {
                dataContext.SetMole(moleIndex, MoleType.None);

                despawnMoleTasks[moleIndex].Stop();
                despawnMoleTasks.Remove(moleIndex);
            }

            return isMoleIndexValid;
        }

        private bool ValidateMoleIndex(int moleIndex)
        {
            if (moleIndex >= 0 && moleIndex < dataContext.Moles.Length)
                return true;

            Debug.LogError($"[LevelMolesGenerator] Mole index validation failed");
            return false;
        }

        public void Dispose()
        {
            generationTask?.Stop();
            isGenerating = false;
        }
    }
}
