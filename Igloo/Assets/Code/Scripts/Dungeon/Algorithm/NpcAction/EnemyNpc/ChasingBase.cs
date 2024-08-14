using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Yarn.Unity;

public class ChasingBase : MonoBehaviour
{
    protected bool playerInSightRange; 
    public float patrolRange, sightRange;

    public LayerMask isPlayer;

    protected Rigidbody rigid;
    protected NavMeshAgent agent;
    protected ActionController player;


    protected void PatrollingOrChasing()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, isPlayer);

        if(playerInSightRange)
        {
            ChasePlayer();
        }
        else //조건고민필요
        {
            Patroling();
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.transform.position);
    }

    //protected virtual void FreezeVelocity()
    //{

    //}

    private void Patroling()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(transform.position, patrolRange, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                agent.SetDestination(point);
            }
        }
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
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

    protected virtual void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

}
