using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleNpcV2 : MonoBehaviour
{
    [SerializeField] private string[] contents;
    [SerializeField] private SpeechBubbleV2 speechBubble;
    [SerializeField] private BoxCollider boxCollider;

    private bool isLeverUp = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowBubble();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseBubble();
        }
    }


    private void ShowBubble()
    {
        speechBubble.target = this.transform;
        speechBubble.gameObject.SetActive(true);
        if (!isLeverUp)
            speechBubble.content = contents[0];
        else
            speechBubble.content = contents[1];

        speechBubble.TextOn();
    }

    private void CloseBubble()
    {
        speechBubble.gameObject.SetActive(false);
        speechBubble.content = "";
        speechBubble.TextOff();
    }

    public void LeverUp()
    {
        isLeverUp = true;
    }

    public void CloseEternally()
    {
        CloseBubble();
        // 활성화 되지 않도록 collider 삭제
        boxCollider.gameObject.SetActive(false);
    }

    public bool IsNowOpen()
    {
        if (speechBubble.gameObject.activeSelf)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
