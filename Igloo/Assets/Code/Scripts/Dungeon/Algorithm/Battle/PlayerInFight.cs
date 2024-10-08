using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInFight : MonoBehaviour
{
    [SerializeField] HealthUI healthUI;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Melee"))
        {

            healthUI.decreaseHealth(50);
            animator.SetTrigger("Hit");
            SoundManager.instance.Play("61_Hit_03", Sound.Effect2, 1, 0.2f);
            
        }
    }
}
