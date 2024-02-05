using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_item : MonoBehaviour
{
    public float pickupRange = 2f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickupItem();
        }
    }

    void TryPickupItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange))
        {
            IItemPickup itemPickup = hit.transform.GetComponent<IItemPickup>();
            if (itemPickup != null)
            {
                itemPickup.PickupItem();
            }
        }
    }
}