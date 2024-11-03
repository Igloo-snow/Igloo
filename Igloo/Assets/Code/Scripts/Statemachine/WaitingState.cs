using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingState : StateMachineBehaviour
{
    private GameObject thisObject;
    private GameObject player;

    public float range; 
    public string triggerSting;
    private int count = 1;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        thisObject = GameObject.FindWithTag("Enemy");
        player = GameObject.FindWithTag("Player");
        count = 1;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(thisObject.transform.position, player.transform.position);
        if(distance <= range && count > 0)
        {
            animator.SetTrigger(triggerSting);
            count--;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

}
