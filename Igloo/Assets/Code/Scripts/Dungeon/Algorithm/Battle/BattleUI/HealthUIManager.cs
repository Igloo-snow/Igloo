using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUIManager : MonoBehaviour
{
    [SerializeField] GameObject battleUI;
    [SerializeField] GameObject bossRoomDoor;

    private void OnEnable()
    {
        GameEventsManager.instance.algoEvents.onAlgoFinalBattleInit += ResetUI;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.algoEvents.onAlgoFinalBattleInit -= ResetUI;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            battleUI.SetActive(true);
            bossRoomDoor.GetComponent<Animator>().SetTrigger("close");
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void ResetUI()
    {
        battleUI.SetActive(false);
        GetComponent<BoxCollider>().enabled = true;
    }
}
