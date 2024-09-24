using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiting2State : StateMachineBehaviour
{
    private GameObject thisObject;
    private GameObject player;

    public float range;
    public string triggerSting;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        thisObject = GameObject.FindWithTag("Enemy");
        player = GameObject.FindWithTag("Player");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector3.Distance(thisObject.transform.position, player.transform.position);
        Debug.Log(distance);
        if (distance <= range)
        {
            animator.SetTrigger(triggerSting);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
