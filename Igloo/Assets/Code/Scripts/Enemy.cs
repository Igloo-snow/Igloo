using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    // ����
    public float timeBetweenAttacks;
    private bool isAttacking;

    //������ �� �ӽ� ����
    public int damage;

    // ����
    private bool playerInSightRange, playerAttackRange;
    private bool isMoving = true;
    public float patrolRange ,sightRange, attackRange;


    public LayerMask isPlayer;
    Vector3 reactVec;

    private Rigidbody rigid;
    CapsuleCollider capsuleCollider;
    Animator anim;
    NavMeshAgent agent;
    ActionController player;
    SphereCollider meleeArea;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<ActionController>();
        meleeArea = GetComponentInChildren<SphereCollider>();
        Debug.Log(meleeArea.gameObject.name);
    }

    private void Update()
    {
        
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);
        playerAttackRange = Physics.CheckSphere(transform.position, attackRange, isPlayer);

        if (!playerInSightRange && !playerAttackRange) Patroling();
        if (playerInSightRange && !playerAttackRange) ChasePlayer();
        if (playerInSightRange && playerAttackRange) TryAttack();

    }

    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    
    private void FreezeVelocity()
    {
        if(isMoving)
        {
            //������ navAgent�� ���ذ� ���� �ʵ���
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
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) 
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private void ChasePlayer()
    {
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

        // ���� �ڵ�
        anim.SetTrigger("IsAttacking");
        yield return new WaitForSeconds(0.3f);

        meleeArea.enabled = true;
        yield return new WaitForSeconds(3f);

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // player�� �ƴ϶� player ����� �����ʿ�
        {
            currentHealth -= damage;
            reactVec = transform.position - collision.transform.position;
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
            Destroy(gameObject, 4f);
        }
        else
        {
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
}
