using System;
using UnityEngine;

namespace MeShineFactory.WhacAMole.Utility
{
    [Serializable]
    public class KeyValueBinding<KeyType, ValueType>
    {
        [field: SerializeField] public KeyType Key { get; private set; }
        [field: SerializeField] public ValueType Value { get; private set; }
    }
}
