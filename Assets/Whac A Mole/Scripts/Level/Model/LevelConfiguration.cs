using System;
using UnityEngine;
using MeShineFactory.WhacAMole.Utility;

namespace MeShineFactory.WhacAMole.Level.Model
{
    [Serializable]
    public class LevelConfiguration
    {
        [field: SerializeField] public float GameDelay { get; private set; }
        [field: SerializeField] public ValueRange SpawnInterval { get; private set; }
        [field: SerializeField] public float DespawnDelay { get; private set; }
    }
}
