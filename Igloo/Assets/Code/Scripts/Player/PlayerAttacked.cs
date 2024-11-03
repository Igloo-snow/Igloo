using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacked : MonoBehaviour
{

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Melee")) // 향후 변경 필요 
        {
            GetComponent<PlayerMovement>().Attacked(hit.transform);
            SoundManager.instance.Play("30_Jump_03");
        }
    }
}
