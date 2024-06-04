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
        [SerializeField] private SimulationMenu _simulationMenu;
        
        private void Awake()
        {
            InitField();
            InitAnimals();
            InitFood();
            _simulation.Init();
            _simulationMenu.Init();
        }

        
        private void InitField()
        {
            GameObject fieldGameObject = Instantiate(_fieldPrefab);

            fieldGameObject.transform.localScale = FieldSize();

            Field field = new Field();
            field.InitField(Constants.FieldSize);
            
            _simulation.SetField(field);
        }

        private void InitAnimals()
        {
            Animal[] animals = new Animal[Constants.AnimalsCount];

            for (int i = 0; i < Constants.AnimalsCount; i++)
            {
                animals[i] = CreateNewAnimal();
                animals[i].SetSpeed(Constants.AnimalsSpeed);
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
            SetAnimal(animal);

            return animal;
        }

        private void SetAnimal(Animal animal)
        {
            int animalId = NewAnimalID();
            animal.ID = animalId;
            animal.GetComponent<MeshRenderer>().material.color = ColorGenerator.GetColorForId(animalId);
        }

        private Vector3 AnimalSpawnPosition()
        {
            Vector3 spawnPosition = EmptyCell().WorldPosition;
            float additionYPosition = 0.5f;
            spawnPosition += new Vector3(0, additionYPosition, 0);
            
            return spawnPosition;
        }

        private Food CreateNewFood()
        {
            Cell emptyCell = EmptyCell();
            emptyCell.CellState = CellState.Food;
            GameObject foodGameObject = Instantiate(_foodPrefab, FoodSpawnPosition(emptyCell), Quaternion.identity);
            Food food = foodGameObject.GetComponent<Food>();
            SetFood(food, emptyCell);
            return food;
        }

        private void SetFood(Food food, Cell emptyCell)
        {
            food.Cell = emptyCell;
            int foodId = NewFoodID();
            food.ID = foodId;
            food.GetComponent<MeshRenderer>().material.color = ColorGenerator.GetColorForId(foodId);
        }

        private static Vector3 FoodSpawnPosition(Cell emptyCell)
        {
            Vector3 spawnPosition = emptyCell.WorldPosition;
            float additionYPosition = 0.3f;
            spawnPosition += new Vector3(0, additionYPosition, 0);
            return spawnPosition;
        }

        private Cell EmptyCell()
        {
            Field field = _simulation.Field;
            Cell emptyCell = field.RandomEmptyCell();
            return emptyCell;
        }

        private int NewFoodID() => 
            Constants.foodIDCounter++;

        private int NewAnimalID() => 
            Constants.AnimalIDCounter++;


        private static Vector3 FieldSize()
        {
            float additionForNavMesh = 1.5f;
            return new(Constants.FieldSize + additionForNavMesh, 1, Constants.FieldSize + additionForNavMesh);
        }
    }
}
