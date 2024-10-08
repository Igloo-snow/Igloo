using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEngine.EventSystems.EventTrigger;

public class BossEnemy : ChasingBase
{
    public enum AttackType { AttackHorizontal,  AttackDown, AttackJump, Cry }
    private Animator animator;

    public bool isAwake = false;
    private bool isAttacking = false;
    public bool tryToAttack = false;
    public GameObject weapon;
    private int cryCount = 1;

    [Header("Range")]
    [SerializeField] private float jumpAttackRange;
    [SerializeField] private float meleeAttackRange;

    [Header("Time")]
    public float currentCooltime = 0f;
    public float rotationSpeed = 5f;

    [Header("Attack")]
    public float attackAngle = 10.0f;
    public float hAttackAngle = 8f;
    public float vAttackAngle = 4f;
    public HealthUI playerHealth;
    public GameObject[] attacks;

    private bool jumpAttack;
    private bool meleeAttack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<ActionController>();
        agent = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        if (!isAwake)   return;

        if(currentCooltime > 0)
        {
            currentCooltime -= Time.deltaTime;
        }
        if (isAttacking) return;

        ChasePlayer();
        animator.SetFloat("Blend", agent.velocity.magnitude);

        if (DistanceFromPlayer() <= meleeAttackRange)
        {
            TryCloseAttack();
        }
        else if (DistanceFromPlayer() <= jumpAttackRange)
        {
            TryFarAttack();
        }

    }

    private void TryCloseAttack()
    {
        if (currentCooltime <= 0)
        {
            if (GetAngleToPlayer() <= vAttackAngle)
                StartAttack((int)AttackType.AttackDown);
            else
                StartAttack((int)AttackType.AttackHorizontal);
            currentCooltime += 3f;
        }
    }

    private void TryFarAttack()
    {
        if (currentCooltime <= 0)
        {
            if (cryCount <= 0)
            {
                StartAttack((int)AttackType.AttackJump);
                return;
            }
            int randomIndex = UnityEngine.Random.Range(2, 4);
            StartAttack(randomIndex);
            currentCooltime += 5f;

        }
    }

    private void StartAttack(int randomIndex)
    {
        animator.SetTrigger(((AttackType)randomIndex).ToString());
    }   

    private float GetAngleToPlayer()
    {
        Vector3 directionToPlayer = player.transform.position - this.transform.position;
        directionToPlayer.y = 0;
        Vector3 enemyForward = transform.forward;
        float angle = Vector3.Angle(enemyForward, directionToPlayer);
        return angle;
    }
    private void SmoothLook()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private float DistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position);

    }

    public void WeaponOn() //WeaponOn 애니메이션에서 호출
    {
        weapon.gameObject.SetActive(true);
        isAwake = true;
    }

    public void WeaponFxOnOff(int type) //공격 애니메이션에서 호출
    {
        isAttacking = !isAttacking;
        agent.enabled = !agent.enabled;
        if (isAttacking)
        {
            attacks[type].GetComponent<IAttackBase>().StartAttack();
        }
        else
        {
            attacks[type].GetComponent<IAttackBase>().FinishAttack();
        }

    }
}
