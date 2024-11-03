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
                Debug.Log("Item picked up: " + heldItem.name);
                break;
            }
        }
    }

    void DropItem()
    {
        if (heldItem != null)
        {
            heldItem.transform.SetParent(null);
            heldItem.transform.position = dropPosition.position;
            Debug.Log("Item dropped: " + heldItem.name);
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
            if (npc != null)
            {
                Debug.Log("NPC found: " + npc.name);
                NPC frontNPC = FindObjectOfType<NPCManager>().GetFrontNPC();
                if (npc == frontNPC)
                {
                    Debug.Log("Front NPC is: " + frontNPC.name);
                    npc.ReceiveItem(heldItem);
                    heldItem = null; // 현재 아이템 제거
                    Debug.Log("Item given to NPC: " + npc.name);
                }
                break;
            }
        }
    }
}