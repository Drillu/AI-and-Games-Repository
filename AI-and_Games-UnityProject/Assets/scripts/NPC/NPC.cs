using Assets.scripts.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.scripts.NPC
{
    [RequireComponent(typeof(NavMeshAgent), typeof(FiniteStateMachine))]
    public class NPC : MonoBehaviour
    {
        NavMeshAgent _navMeshAgent;
        FiniteStateMachine _finiteStateMachine;
        public void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _finiteStateMachine = GetComponent<FiniteStateMachine>();
        }

        public void Start()
        {
            
        }

        public void Update()
        {
            
        }
    }
}
