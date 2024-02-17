using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어 게이트 인식");
            GameEventsManager.instance.playerEvents.NextStage();
        }
    }
}
