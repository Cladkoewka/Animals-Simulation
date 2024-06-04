using System;
using CodeBase.FieldLogic;
using UnityEngine;

namespace CodeBase.AnimalsLogic
{
    public class Food : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystemPrefab;
        public int ID { get; set; }
        public Cell Cell { get; set; }
        public event Action<Food> OnFoodCollect;

        private Field _field;
        

        public void Collect()
        {
            CreateParticles();
            OnFoodCollect?.Invoke(this);
        }

        private void CreateParticles() => 
            Instantiate(_particleSystemPrefab, transform.position, Quaternion.identity);
        
    }
}