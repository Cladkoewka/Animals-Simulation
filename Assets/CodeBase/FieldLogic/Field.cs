using System;
using System.Collections.Generic;
using CodeBase.AnimalsLogic;
using Unity.AI.Navigation;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.FieldLogic
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface _meshSurface;
        
        private Cell[,] _field;

        public void InitField(int fieldSize)
        {
            _field = new Cell[fieldSize, fieldSize];

            InitCells(fieldSize);
        }

        public void Build() => 
            _meshSurface.BuildNavMesh();

        private void Start()
        {
            Build();
        }


        public Cell RandomEmptyCell()
        {
            List<Cell> emptyCells = new List<Cell>();

            foreach (var cell in _field)
            {
                if (cell.CellState == CellState.Empty) 
                    emptyCells.Add(cell);
            }

            if (emptyCells.Count > 0)
            {
                int randomIndex = Random.Range(0, emptyCells.Count);
                return emptyCells[randomIndex];
            }
            else
                return null;
        }

        public Cell RandomEmptyCell(Cell currentCell)
        {
            List<Cell> emptyCells = new List<Cell>();

            foreach (var cell in _field)
            {
                if (IsCellAvaliable(cell, currentCell)) 
                    emptyCells.Add(cell);
            }

            if (emptyCells.Count > 0)
            {
                int randomIndex = Random.Range(0, emptyCells.Count);
                return emptyCells[randomIndex];
            }
            else
                return null;
        }

        private static bool IsCellAvaliable(Cell cell, Cell currentCell)
        {
            return 
                cell.CellState == CellState.Empty && 
                Vector3.Distance(cell.WorldPosition, currentCell.WorldPosition) <= Constants.AnimalsSpeed * 5;
        }

        private void InitCells(int fieldSize)
        {
            for (int i = 0; i < fieldSize; i++)
            {
                for (int j = 0; j < fieldSize; j++)
                {
                    _field[i, j] = new Cell(WorldPosition(i, j, fieldSize), CellState.Empty);
                }
            }
        }

        private Vector3 WorldPosition(int x, int z, int fieldSize)
        {
            float cellSize = 1.0f;

            float worldX = (x * cellSize) - ((fieldSize * cellSize) / 2.0f) + (cellSize / 2.0f);
            float worldZ = (z * cellSize) - ((fieldSize * cellSize) / 2.0f) + (cellSize / 2.0f);
            float worldY = 0;

            return new Vector3(worldX, worldY, worldZ);
        }
    }
}