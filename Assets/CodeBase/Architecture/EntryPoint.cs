using System;
using CodeBase.AnimalsLogic;
using CodeBase.FieldLogic;
using Unity.AI.Navigation;
using UnityEngine;

namespace CodeBase.Architecture
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Simulation _simulation;
        [SerializeField] private GameObject _fieldPrefab;
        [SerializeField] private GameObject _animalPrefab;
        [SerializeField] private GameObject _foodPrefab;
        
        private void Awake()
        {
            InitField();
            InitFood();
            InitAnimals();
        }



        private void InitField()
        {
            GameObject fieldGameObject = Instantiate(_fieldPrefab);

            fieldGameObject.transform.localScale = FieldSize();

            Field field = new Field(Constants.FieldSize);
            
            
            _simulation.SetField(field);
        }

        private void InitAnimals()
        {
            Animal[] animals = new Animal[Constants.AnimalsCount];

            for (int i = 0; i < Constants.AnimalsCount; i++)
            {
                animals[i] = CreateNewAnimal();
                animals[i].SetFood(_simulation.GetFood(i));
            }
                
            
            _simulation.SetAnimals(animals);
            
            
        }

        private void InitFood()
        {
            Food[] foods = new Food[Constants.AnimalsCount];

            for (int i = 0; i < Constants.AnimalsCount; i++) 
                foods[i] = CreateNewFood();
            
            _simulation.SetFood(foods);
        }

        private Animal CreateNewAnimal()
        {
            GameObject animalGameObject = Instantiate(_animalPrefab, AnimalSpawnPosition(), Quaternion.identity);
            Animal animal = animalGameObject.GetComponent<Animal>();
            int animalId = NewAnimalID();
            animal.ID = animalId;
            animalGameObject.GetComponent<MeshRenderer>().material.color = ColorGenerator.GetColorForId(animalId);
            
            return animal;
        }

        private Food CreateNewFood()
        {
            GameObject foodGameObject = Instantiate(_foodPrefab, FoodSpawnPosition(), Quaternion.identity);
            Food food = foodGameObject.GetComponent<Food>();
            int foodId = NewFoodID();
            food.ID = foodId;
            foodGameObject.GetComponent<MeshRenderer>().material.color = ColorGenerator.GetColorForId(foodId);

            return food;
        }

        private int NewFoodID() => 
            Constants.foodIDCounter++;

        private int NewAnimalID() => 
            Constants.AnimalIDCounter++;

        private Vector3 AnimalSpawnPosition()
        {
            Vector3 spawnPosition = AnimalSpawnCell().WorldPosition;
            float additionYPosition = 0.5f;
            spawnPosition += new Vector3(0, additionYPosition, 0);
            
            return spawnPosition;
        }

        private Vector3 FoodSpawnPosition()
        {
            Vector3 spawnPosition = FoodSpawnCell().WorldPosition;
            float additionYPosition = 0.3f;
            spawnPosition += new Vector3(0, additionYPosition, 0);
            
            return spawnPosition;
        }

        private Cell AnimalSpawnCell()
        {
            Field field = _simulation.Field;
            Cell emptyCell = field.RandomEmptyCell();
            emptyCell.CellState = CellState.Animal;
            return emptyCell;
        }

        private Cell FoodSpawnCell()
        {
            Field field = _simulation.Field;
            Cell emptyCell = field.RandomEmptyCell();
            emptyCell.CellState = CellState.Food;
            return emptyCell;
        }

        private static Vector3 FieldSize()
        {
            float additionForNavMesh = 1.5f;
            return new(Constants.FieldSize + additionForNavMesh, 1, Constants.FieldSize + additionForNavMesh);
        }
    }
}
