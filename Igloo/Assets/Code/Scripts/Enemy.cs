using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    // 공격
    public float timeBetweenAttacks;
    private bool isAttacking;

    //데미지 값 임시 변수
    public int damage;

    // 상태
    private bool playerInSightRange, playerAttackRange;
    private bool isMoving = true, isDead = false;
    public float patrolRange ,sightRange, attackRange;


    public LayerMask isPlayer;
    Vector3 reactVec;

    private Rigidbody rigid;
    Animator anim;
    NavMeshAgent agent;
    ActionController player;
    SphereCollider meleeArea;

    [SerializeField] private GameObject obstruction;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        //capsuleCollider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<ActionController>();
        meleeArea = GetComponentInChildren<SphereCollider>();
    }

    private void Update()
    {
        
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);

        if (!isDead)
        {
            if (!playerInSightRange && !playerAttackRange) Patroling();
            if (playerInSightRange && !playerAttackRange) ChasePlayer();
            if (playerInSightRange && playerAttackRange) TryAttack();
        }


    }

    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    private void FreezeVelocity()
    {
        if(isMoving)
        {
            //물리가 navAgent에 방해가 되지 않도록
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    private void Patroling()
    {
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if(RandomPoint(transform.position, patrolRange, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                agent.SetDestination(point);
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) 
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private void ChasePlayer()
    {
        Debug.Log("Chase");

        agent.SetDestination(player.transform.position);
    }

    private void TryAttack()
    {
        if (!isAttacking)
        {
            StartCoroutine(AttackPlayer());
        }
    }

    IEnumerator AttackPlayer()
    {
        isAttacking = true;
        agent.SetDestination(transform.position);
        transform.LookAt(player.transform);

        // 공격 코드
        anim.SetTrigger("IsAttacking");
        yield return new WaitForSeconds(0.3f);

        meleeArea.enabled = true;
        yield return new WaitForSeconds(1f);

        meleeArea.enabled = false;
        isAttacking = false;
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Melee"))
        {
            currentHealth -= damage;
            reactVec = transform.position - other.transform.position;
            StartCoroutine(OnDamage());
        }
    }

    IEnumerator OnDamage()
    {
        yield return new WaitForSeconds(0.1f);

        if(currentHealth <= 0)
        {
            anim.SetBool("IsDead", true);
            gameObject.layer = LayerMask.NameToLayer("EnemyDead");

            isMoving = false;

            reactVec = reactVec.normalized;
            rigid.AddForce(reactVec * 8, ForceMode.Impulse);
            agent.enabled = false;
            if (obstruction != null)
                PathOpen();
            Destroy(gameObject, 4f);
        }
        else
        {
            isDead = true;
            anim.SetBool("IsAttacked", true);
            reactVec = reactVec.normalized;
            rigid.AddForce(reactVec * 4, ForceMode.Impulse);
            float originalMass = rigid.mass;
            rigid.mass *= 2;
            yield return new WaitForSeconds(1f);
            anim.SetBool("IsAttacked", false);
            rigid.mass = originalMass;
            
        }
    }

    private void PathOpen()
    {
        Destroy(obstruction);
    }
}
