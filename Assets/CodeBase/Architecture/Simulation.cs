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

        public void Init()
        {
            SetAnimalsFood();
            SubscribeAllFood();
        }

        private void SubscribeAllFood()
        {
            foreach (Food food in _foods) 
                food.OnFoodCollect += MoveFood;
        }

        private void MoveFood(Food food)
        {
            
            food.Cell.CellState = CellState.Empty;
            Cell newCell = _field.RandomEmptyCell(food.Cell);

            food.Cell = newCell;
            food.transform.position = FoodSpawnPosition(newCell);
        }


        private void Update()
        {
            Tick();
        }

        private void SetAnimalsFood()
        {
            foreach (Animal animal in _animals)
            {
                foreach (Food food in _foods)
                {
                    if (food.ID == animal.ID)
                    {
                        animal.Food = food;
                    }
                }
            }
        }

        private void Tick()
        {
            for (int i = 0; i < Constants.AnimalsCount; i++)
            {
                _animals[i].Move();
            }
        }

        private static Vector3 FoodSpawnPosition(Cell emptyCell)
        {
            Vector3 spawnPosition = emptyCell.WorldPosition;
            float additionYPosition = 0.3f;
            spawnPosition += new Vector3(0, additionYPosition, 0);
            return spawnPosition;
        }
        
        public void SetField(Field field) => 
            _field = field;

        public void SetAnimals(Animal[] animals) => 
            _animals = animals;

        public void SetFood(Food[] foods) => 
            _foods = foods;
    }
}