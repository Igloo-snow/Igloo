using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class StageGraph : MonoBehaviour
{
    [Header("Graph")]
    [SerializeField] private Node[] nodes;
    [SerializeField] private Edge[] edges;
    private int previousNode = -1;

    [Header("Stage")]
    [SerializeField] private Transform fallingObjparent;
    private GameObject[] fallingObjs;
    public Vector3 startPos;
    public float shadowTime = 2f;

    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onStepNode += StepNode;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onStepNode -= StepNode;
    }

    void Awake()
    {
        startPos = transform.position;

        fallingObjs = new GameObject[fallingObjparent.childCount];
        for (int i = 0; i < fallingObjparent.childCount; i++)
        {
            fallingObjs[i] = fallingObjparent.GetChild(i).gameObject;
        }
    }

    private void StepNode(int i)
    {
        if(previousNode == -1)
        {
            previousNode = i;
        }
        if(previousNode == i)
        {
            return;
        }
        else
        {
            //previoudNode 랑 i랑 가지고 edge 확인
            for(int j =  0; j < edges.Length; j++)
            {
                if (edges[j].CheckEdge(Mathf.Min(previousNode, i), Mathf.Max(previousNode, i)))
                {
                    CheckFinish();
                }
            }
            previousNode = i;
        }

    }

    public bool CheckFinish()
    {
        for(int i = 0; i < edges.Length; i++)
        {
            if (!edges[i].isPassed)
            {
                return false;
            }
        }

        GameEventsManager.instance.playerEvents.StageFinish();
        return true;
    }

    public void Restart()
    {
        ResetFallingObj();
        ResetColor();
        previousNode = -1;
        foreach(Edge edge in edges)
        {
            edge.isPassed = false;
        }
    }

    private void ResetFallingObj()
    {
        //fallin obj 재설정 관리
        for (int i = 0; i < fallingObjs.Length; i++)
        {
            fallingObjs[i].gameObject.SetActive(true);

            fallingObjs[i].GetComponent<FallingObject>().Rearrange();
        }
    }

    private void ResetColor()
    {
        for(int i = 0; i < edges.Length; i++)
        {
            edges[i].colorLine.GetComponent<MeshRenderer>().material.color = Color.red;
        }
        for(int i = 0; i < nodes.Length; i++)
        {
            nodes[i].nodeLight.SetColor();
        }
    }
}
