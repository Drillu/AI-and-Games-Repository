using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.scripts.FSM.newStates
{
    [CreateAssetMenu(fileName ="IdleState", menuName ="States/Idle", order = 1)]
    public class IdleState : abstractFSMState
    {
        public override bool EnterState()
        {
            base.EnterState();

            Debug.Log("ENTERED IDLE STATE");
            return true;
        }
        public override void UpdateState()
        {
            Debug.Log("UPDATEING IDLE STATE");
        }
        public override bool ExitState()
        {
            base.ExitState();

            Debug.Log("EXITED IDLE STATE");
            return true;
        }
    }
}
