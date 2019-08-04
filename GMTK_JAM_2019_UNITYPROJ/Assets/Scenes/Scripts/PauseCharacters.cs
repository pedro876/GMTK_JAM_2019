using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseCharacters : StateMachineBehaviour
{
    [SerializeField] bool pauseMovement = true;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CharacterController.canMove = !pauseMovement;
        SurfboardController.canMove = !pauseMovement;
    }
}
