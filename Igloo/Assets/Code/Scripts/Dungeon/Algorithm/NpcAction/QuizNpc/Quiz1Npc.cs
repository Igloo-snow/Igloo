using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Quiz1Npc : AlgoNpcAction
{
    [SerializeField] Animator animator;

    [SerializeField] GameObject keyPrefab;
    [SerializeField] Transform keyPosition;

    private int count = 1;

    protected override void ActionStart()
    {
        if(count > 0)
        {
            animator.SetTrigger("Touch");
            Instantiate(keyPrefab, keyPosition.position, Quaternion.identity);
            count--;
        }

    }
}
