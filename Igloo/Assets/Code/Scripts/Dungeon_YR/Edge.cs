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
    public GameObject colorLine;


    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player") && isCrossed)
        {
            Debug.Log("플레이어 인식");
            crossEdge.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player") && isCrossed)
        {
            Debug.Log("플레이어 아웃 인식");
            crossEdge.gameObject.SetActive(true);
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
            return isPassed;
        }
        return false;
    }

}
