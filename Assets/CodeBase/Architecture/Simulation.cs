using System;
using System.Collections.Generic;
using CodeBase.AnimalsLogic;
using CodeBase.FieldLogic;
using UnityEngine;

namespace CodeBase.Architecture
{
    public class Simulation : MonoBehaviour
    {
        private Field _field;
        private Animal[] _animals;
        private Food[] _foods;

        public Field Field => _field;

        private void Update()
        {
            Tick();
        }

        private void Tick()
        {
            for (int i = 0; i < Constants.AnimalsCount; i++)
            {
                _animals[i].Move();
            }
        }

        public void SetField(Field field) => 
            _field = field;

        public void SetAnimals(Animal[] animals) => 
            _animals = animals;

        public void SetFood(Food[] foods) => 
            _foods = foods;
        
        public Food GetFood(int id)
        {
            return _foods[id];
        }
    }
}