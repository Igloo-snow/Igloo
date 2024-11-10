using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class BossEnemy : ChasingBase ,ICharacterState
{
    public enum AttackType { AttackHorizontal,  AttackDown, AttackJump, Cry }
    private Animator animator;

    public bool isAwake = false;
    private bool isAttacking = false;
    public bool tryToAttack = false;
    public GameObject weapon;
    private int cryCount = 1;
    private int finishAnimCallCount = 1;

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

    [Header("Health")]
    private int maxHealth = 500;
    [SerializeField] HealthUI healthUI;

    [Header("Finish")]
    public Vector3 startPos;
    public Quaternion startRotation;
    public bool isFinish = false;

    private bool jumpAttack;
    private bool meleeAttack;

    public int GetHealth()
    {
        return maxHealth;
    }

    public void Die()
    {
        //아직 구현 전.
    }
    private void OnEnable()
    {
        GameEventsManager.instance.algoEvents.onAlgoFinalBattleFinish += FinishBattle;
        GameEventsManager.instance.algoEvents.onAlgoFinalBattleInit += InitBoss;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.algoEvents.onAlgoFinalBattleFinish -= FinishBattle;
        GameEventsManager.instance.algoEvents.onAlgoFinalBattleInit -= InitBoss;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").GetComponent<ActionController>();
        agent = GetComponent<NavMeshAgent>();
        startPos = transform.position;
        startRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerWeapon"))
        {
            Debug.Log("플레이어 인식 확인");
            healthUI.decreaseHealth(50);
        }
    }

    private void Update()
    {
        if (!isAwake)   return;
        if (isFinish)
        {
            //agent.SetDestination(startPos);
            //agent.SetDestination(player.transform.position);
            animator.SetFloat("Blend", agent.velocity.magnitude);

            if (MathF.Abs(transform.position.x - startPos.x) < 6f && finishAnimCallCount > 0)
            {
                transform.rotation = startRotation;
                animator.SetTrigger("BattleFinish");
                finishAnimCallCount--; // 이후 리셋시 초기화 필요
            }
            return;
        }

        if(currentCooltime > 0)
        {
            currentCooltime -= Time.deltaTime;
        }
        if (isAttacking) return;
        if (agent.enabled)
        {
            ChasePlayer();
        }
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

    private void FinishBattle()
    {
        isFinish = true;
        finishAnimCallCount = 1;

    }

    private void InitBoss()
    {
        if (agent.enabled == false)
        {
            agent.enabled = true;
        }
        isAwake = false;
        isFinish = false;
        animator.SetTrigger("StartAgain");
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
