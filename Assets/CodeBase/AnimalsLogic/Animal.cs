using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace CodeBase.AnimalsLogic
{
    public class Animal : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        public int ID { get; set; }
        
        public Food Food { get; set; }


        public void SetSpeed(int speed) => 
            _agent.speed = speed;

        public void Move()
        {
            _agent.destination = Food.transform.position;
            if (_agent.remainingDistance <= Constants.CollectDistance)
            {
                Food.Collect();
            }
        }

    }
}