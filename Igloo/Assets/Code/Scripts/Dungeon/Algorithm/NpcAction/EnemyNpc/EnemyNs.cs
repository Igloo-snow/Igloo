using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNs : ChasingBase
{
    private Animator anim;

    private bool playerInSlowEffectRange;
    //[SerializeField] private float slowEffectRange;
    [SerializeField] private float slowRatio = 0.3f;
    [SerializeField] private Vector3 centerPos;


    private void Awake()
    {
        base.rigid = GetComponent<Rigidbody>();
        base.agent = GetComponent<NavMeshAgent>();
        base.player = FindObjectOfType<ActionController>();
        anim = GetComponent<Animator>();
        centerPos = transform.position;
    }

    private void Update()
    {
        if (CheckPlayerInSightRange(centerPos))
        {
            ChasePlayer();
        }
        else
        {
            Patroling();
        }

        StayInArea(centerPos);
    }


    //protected override void OnDrawGizmosSelected()
    //{
    //    base.OnDrawGizmosSelected(); // (color yellow,sightRange)
    //    //Gizmos.color = Color.red;
    //    //Gizmos.DrawWireSphere(transform.position, patrolRange);
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, slowEffectRange);
    //    Gizmos.color = Color.black;
    //    Gizmos.DrawWireSphere(centerPos, patrolRange);
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SlowEffect();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ResetSpeed();
        }
    }

    private void SlowEffect()
    {
        //플레이어 디버프
        player.GetComponent<PlayerMovement>().ChangeSpeed(slowRatio);
        //사운드 효과
        SoundManager.instance.ChangeBgmSpeed(0.9f);
        SoundManager.instance.Play("26_Swim_Submerged_02", Sound.Effect);

    }

    public void ResetSpeed()
    {
        player.GetComponent<PlayerMovement>().ChangeSpeed(1f);
        SoundManager.instance.ChangeBgmSpeed(1f);

    }
}