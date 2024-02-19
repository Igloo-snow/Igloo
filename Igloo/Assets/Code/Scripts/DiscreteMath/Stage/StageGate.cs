using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("�÷��̾� ����Ʈ �ν�");
            GameEventsManager.instance.playerEvents.NextStage();
        }
    }
}
