using System;
using UnityEngine;
using MeShineFactory.WhacAMole.Level.Model;

namespace MeShineFactory.WhacAMole.Level.View
{
    public class MoleViewer : MonoBehaviour
    {
        [field: SerializeField] public MoleType MoleType;

        public int ID { get; set; }

        public event Action<int> OnBeingHitted;

        private void OnMouseDown()
        {
            OnBeingHitted?.Invoke(ID);
        }

        public void Disappear()
        {
            Destroy(gameObject);
        }
    }
}
