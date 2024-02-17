using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public GameObject[] nodes;
    public GameObject edgePrefab;
    public GameObject[] edges;
    
    private int edgeIndex;
    private float dist;

    private void Start()
    {

    }

    public void ConnectNodes(int n1, int n2)
    {
        GameObject node1 = nodes[n1];
        GameObject node2 = nodes[n2];
        dist = Vector3.Distance(node1.transform.position, node2.transform.position);
        Debug.Log(dist);
        edges[edgeIndex] = Instantiate(edgePrefab, this.transform);

        //현재 x축이 길어지는 방식으로 코드 구현
        edges[edgeIndex].transform.localScale = new Vector3(dist, edges[edgeIndex].transform.localScale.y, edges[edgeIndex].transform.localScale.z);

        Vector3 StartPoint = node1.transform.position;
        edges[edgeIndex].transform.position = StartPoint;

        Vector3 rotationDirection = (node2.transform.position - node1.transform.position);
        edges[edgeIndex].transform.right = rotationDirection;  
    }


}
