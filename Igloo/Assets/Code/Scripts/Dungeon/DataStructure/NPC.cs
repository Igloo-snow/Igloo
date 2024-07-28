using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject desiredItem;
    public float interactionDistance = 1.0f;

    private bool isInteracting = false;

    void Update()
    {
        if (!isInteracting && desiredItem != null)
        {
            float distance = Vector3.Distance(transform.position, desiredItem.transform.position);
            Debug.Log($"Distance to desired item: {distance}, Interaction Distance: {interactionDistance}");

            if (distance < interactionDistance)
            {
                if (IsFrontNPC())
                {
                    InteractWithItem();
                }
                else
                {
                    Debug.Log("Not front NPC");
                }
            }
        }
    }

    bool IsFrontNPC()
    {
        // NPCSpawner���� �����ϴ� ù ��° NPC�� ��ġ�� ������
        Vector3 firstNPCPosition = NPCSpawner.Instance.GetNPCs()[0].transform.position;

        // ���� NPC�� ��ġ�� ù ��° NPC�� ��ġ�� z �� ��
        bool isFront = Mathf.Approximately(transform.position.z, firstNPCPosition.z);
        Debug.Log($"IsFrontNPC: {isFront}, NPC Position: {transform.position.z}, Front NPC Z Position: {firstNPCPosition.z}");
        return isFront;
    }

    void InteractWithItem()
    {
        isInteracting = true;
        Debug.Log("Interacting with item and destroying NPC.");
        Destroy(desiredItem);
        Destroy(gameObject);
        StartCoroutine(NPCSpawner.Instance.ShiftNPCsForwardWithDelay(2f, 1f));
    }
}