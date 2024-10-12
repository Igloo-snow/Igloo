using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public float knockbackForce = 20f;
    public float knockbackDuration = 0.2f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            
            Debug.Log("플레이어인식1");
            Animator anim = collision.gameObject.GetComponent<Animator>();
            anim.SetTrigger("Jump");

        }
    }
}
