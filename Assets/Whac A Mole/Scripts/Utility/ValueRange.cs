using System;
using UnityEngine;

namespace MeShineFactory.WhacAMole.Utility
{
    [Serializable]
    public class ValueRange
    {
        [field: SerializeField] public float Value1 { get; private set; }
        [field: SerializeField] public float Value2 { get; private set; }

        public float Min => Mathf.Min(Value1, Value2);
        public float Max => Mathf.Max(Value1, Value2);

        public float GetRandomValue()
        {
            return UnityEngine.Random.Range(Min, Max);
        }
    }
}
