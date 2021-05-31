using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.scripts.FSM
{
    public class FiniteStateMachine : MonoBehaviour
    {
        [SerializeField]
        abstractFSMState _startingState;

        abstractFSMState _currentState;

        private void Awake()
        {
            _currentState = null;
        }

        public void Start()
        {
            if(_startingState != null)
            {
                EnterState(_startingState);
            }
        }

        private void Update()
        {
            if(_currentState != null)
            {
                _currentState.UpdateState();
            }
        }

        #region START MANAGEMENT
        public void EnterState(abstractFSMState nextState)
        {
            if(nextState == null)
            {
                return;
            }
            _currentState = nextState;
            _currentState.EnterState();
        }
        #endregion

    }
}

