using System;
using System.Collections.Generic;
using UnityEngine;
using MeShineFactory.WhacAMole.Level.Controller;
using MeShineFactory.WhacAMole.Level.Model;

namespace MeShineFactory.WhacAMole.Level.View
{
    public class LevelView : MonoBehaviour, ILevelView
    {
        [SerializeField] private MoleViewer molePrefab;
        [SerializeField] private List<MoleHole> holes;
        [SerializeField] private List<MolePrefabBinding> molesPrefabs;

        private ILevelModel levelModel;
        private MoleViewer[] moles;

        public event Action<int> OnMoleBeingTouched;

        public void Setup(ILevelModel levelModel)
        {
            this.levelModel = levelModel;
            moles = new MoleViewer[levelModel.HolesNumber];

            levelModel.OnMoleUpdated += OnMoleUpdated;
        }

        private void OnMoleUpdated(int moleIndex, MoleType moleType)
        {
            Debug.Log($"[LevelView] mole updated. Index='{moleIndex}', Type='{moleType}'");

            if (ValidateMoleIndex(moleIndex))
            {
                if (moleType is MoleType.None && moles[moleIndex] is not null)
                    FreeMole(moleIndex);
                else if (moles[moleIndex] is null && moleType is not MoleType.None)
                    SpawnMole(moleIndex, moleType);
            }
        }

        private void FreeMole(int moleIndex)
        {
            if (moles[moleIndex] is not null)
            {
                MoleViewer mole = moles[moleIndex];

                mole.OnBeingHitted -= OnMoleTouched;
                mole.Disappear();
                moles[moleIndex] = null;
            }
        }

        private void OnMoleTouched(int moleIndex)
        {
            OnMoleBeingTouched?.Invoke(moleIndex);
        }

        private void SpawnMole(int moleIndex, MoleType moleType)
        {
            MoleViewer prefab = PickMolePrefab(moleType);

            if (prefab != null)
            {
                MoleHole hole = holes[moleIndex];
                MoleViewer mole = Instantiate(prefab);
                mole.transform.position = hole.transform.position;

                mole.ID = moleIndex;
                mole.OnBeingHitted += OnMoleTouched;

                moles[moleIndex] = mole;
            }
        }

        private MoleViewer PickMolePrefab(MoleType moleType)
        {
            MoleViewer prefab = molesPrefabs.Find(p => p.Key == moleType)?.Value;

            if (prefab is null)
                Debug.LogError($"[LevelView] failed to find mole prefab of type '{moleType}'");

            return prefab;
        }

        private bool ValidateMoleIndex(int moleIndex)
        {
            if (moleIndex >= 0 && moleIndex < moles.Length)
                return true;

            Debug.LogError($"[LevelView] mole index validation failed");
            return false;
        }

        public void Dispose()
        {
            levelModel.OnMoleUpdated -= OnMoleUpdated;
        }
    }
}
