using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public Transform target;

    //데미지 값 임시 변수
    public int damage;

    private bool isChase = true;

    Vector3 reactVec;

    private Rigidbody rigid;
    CapsuleCollider capsuleCollider;
    Animator anim;
    NavMeshAgent nav;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(isChase)
        {
            nav.SetDestination(target.position);
        }

    }

    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    
    private void FreezeVelocity()
    {
        if(isChase)
        {
            //물리가 navAgent에 방해가 되지 않도록
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentHealth -= damage;
            reactVec = transform.position - collision.transform.position;
            StartCoroutine(OnDamager());
        }

    }

    IEnumerator OnDamager()
    {
        yield return new WaitForSeconds(0.1f);

        if(currentHealth <= 0)
        {
            anim.SetBool("IsDead", true);
            gameObject.layer = LayerMask.NameToLayer("EnemyDead");

            isChase = false;

            reactVec = reactVec.normalized;
            rigid.AddForce(reactVec * 8, ForceMode.Impulse);
            nav.enabled = false;
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
