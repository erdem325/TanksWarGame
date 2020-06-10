using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : StateMachineBehaviour
{
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<EnemyAI>().SetLoookRotation();
        animator.gameObject.GetComponent<EnemyAI>().Shoot();
    }
}
