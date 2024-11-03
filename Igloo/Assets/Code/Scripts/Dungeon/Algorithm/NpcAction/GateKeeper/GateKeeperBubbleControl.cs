using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeeperBubbleControl : MonoBehaviour
{
    [SerializeField] GameObject bubble;
    [SerializeField] Transform player;
    [SerializeField] float bubbleOnRange;
    private float distance;
    private bool playerInCloseRange = false;

    private void Update()
    {
        if(playerInCloseRange) { return;  }
        distance = Vector3.Distance(transform.position, player.position);
        if (distance <= bubbleOnRange)
        {
            ShowBubble();
        }
        else
        {
            CloseBubble();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInCloseRange = true; // 다시 false로 초기화 되지 않음. 일회성 버블임.
            CloseBubble();
        }
    }

    private void CloseBubble()
    {
        bubble.SetActive(false);
    }

    private void ShowBubble()
    {
        bubble.SetActive(true);
    }

}
