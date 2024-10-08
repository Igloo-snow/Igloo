using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : MonoBehaviour, IAttackBase
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] ActionController player;

    public void StartAttack()
    {
        SmoothLook();
        StartCoroutine(HitGoundCoroutine());
    }

    public void FinishAttack()
    {
    }

    IEnumerator HitGoundCoroutine() //AttackJump 애니메이션에서 호출
    {
        yield return new WaitForSeconds(0.4f);
        
        SoundManager.instance.Play("SFX-Spell-01-Earth_wav");
        particle.Play();

    }

    private void SmoothLook()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
    }
}
