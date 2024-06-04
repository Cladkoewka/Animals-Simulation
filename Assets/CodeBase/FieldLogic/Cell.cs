using System;
using UnityEngine;

namespace CodeBase.FieldLogic
{
    public class Cell
    {
        private Vector3 _worldPosition;
        public CellState CellState { get; set; }
        public Vector3 WorldPosition => _worldPosition;


        public Cell(Vector3 worldPosition, CellState cellState)
        {
            _worldPosition = worldPosition;
            CellState = cellState;
        }

        public void PrintInfo()
        {
            Debug.Log($"{_worldPosition.x} - {_worldPosition.z}");
        }
    }
}