using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Edge : MonoBehaviour
{
    public Node startNode;
    public Node endNode;
    public bool isPassed = false;
    [SerializeField] private bool isCrossed;
    [SerializeField] private GameObject crossEdge;
    [SerializeField] private GameObject[] obstructions;
    public GameObject colorLine;
    [SerializeField] private bool isPassing;


    private void OnTriggerEnter(Collider other)
    {
        isPassing = startNode.isStepped || endNode.isStepped;
        if (other.transform.CompareTag("Player") && isCrossed && isPassing)
        {
            Debug.Log("플레이어 인식");
            for(int i = 0; i < obstructions.Length; i++)
            {
                obstructions[i].SetActive(true);
            }
        }
    }

    public bool CheckEdge(int a, int b)
    {
        int min = Mathf.Min(startNode.nodeId, endNode.nodeId);
        int max = Mathf.Max(startNode.nodeId, endNode.nodeId);
        if(a == min && b == max)
        {
            isPassed = true;
            Debug.Log(this.gameObject.name + " passed");
            colorLine.GetComponent<MeshRenderer>().material.color = Color.gray;
            for (int i = 0; i < obstructions.Length; i++)
            {
                obstructions[i].SetActive(false);
            }
            return isPassed;
        }
        return false;
    }

}
