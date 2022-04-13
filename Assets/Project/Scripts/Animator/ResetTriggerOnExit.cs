using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.AnimatorState
{
    public class ResetTriggerOnExit : StateMachineBehaviour
    {
        [SerializeField]
        private List<string> TriggerName = new List<string>();
        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (var i in TriggerName)
                animator.ResetTrigger(i);
        }
    }
}