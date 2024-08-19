using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPlayer : MonoBehaviour
{
    public Transform holdPosition;
    public Transform dropPosition;
    private GameObject heldItem;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (heldItem == null)
            {
                TryPickUpItem();
            }
            else
            {
                DropItem();
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            TryGiveItemToNPC();
        }
    }

    void TryPickUpItem()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Item"))
            {
                heldItem = hitCollider.gameObject;
                heldItem.transform.position = holdPosition.position;
                heldItem.transform.SetParent(holdPosition);
                break;
            }
        }
    }

    void DropItem()
    {
        if (heldItem != null)
        {
            heldItem.transform.SetParent(null);
            heldItem.transform.position = dropPosition.position; // dropPosition 위치로 이동
            heldItem = null;
        }
    }

    void TryGiveItemToNPC()
    {
        if (heldItem == null) return;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (var hitCollider in hitColliders)
        {
            NPC npc = hitCollider.GetComponent<NPC>();
            if (npc != null && npc == FindObjectOfType<NPCManager>().GetFrontNPC())
            {
                if (npc.IsItemCorrect(heldItem))
                {
                    npc.ReceiveItem(heldItem);
                    heldItem = null;
                }
                break;
            }
        }
    }
}