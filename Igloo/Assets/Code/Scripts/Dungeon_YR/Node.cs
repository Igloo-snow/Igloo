using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int nodeId;
    public int[] adjNodes;
    public bool isStepped = false;
    public NodeLight nodeLight;

    private void Start()
    {
        nodeLight = GetComponentInChildren<NodeLight>();
    }
    public int GetDegree()
    {
        return adjNodes.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {  
            isStepped = true;
            GameEventsManager.instance.playerEvents.StepNode(nodeId);
        }
    }

}
