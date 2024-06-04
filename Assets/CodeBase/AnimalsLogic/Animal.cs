using System;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.AnimalsLogic
{
    public class Animal : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        private Food _food;
        public int ID { get; set; }

        public void SetFood(Food food) => 
            _food = food;

        public void Move()
        {
            _agent.Move(_food.transform.position * 0.001f);
        }

    }
}