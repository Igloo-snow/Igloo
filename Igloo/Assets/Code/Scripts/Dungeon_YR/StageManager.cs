using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class StageManager : MonoBehaviour
{
    struct myGraph
    {
        public int numV;
        public int numE;
        public List<int>[] vArray;
    }
    static myGraph graph = new myGraph();

    private Stage stage;

    private int level = 1;

    // Start is called before the first frame update
    void Start()
    {
        stage = GetComponent<Stage>();

        GraphInit(stage.nodes.Length, stage.edges.Length);
        //AddEdge

        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    private  KeyValuePair<int, int> MakeRandom()
    {
        int a = Random.Range(0, graph.numV);
        int b = Random.Range(0, graph.numV);
        while (a == b) { b = Random.Range(0, graph.numV); }

        
        KeyValuePair<int, int> intPair = new KeyValuePair<int, int>(Mathf.Min(a, b), Mathf.Max(a, b));

        while((graph.vArray[Mathf.Min(a, b)].Contains(Mathf.Max(a, b))))
        {

        }
        return intPair;
    }*/

    public void GraphInit(int numV, int numE)
    {
        graph = new myGraph();
        graph.numV = numV;
        graph.numE = numE;
        graph.vArray = new List<int>[numV];
        for (int i = 0; i < numV; i++)
        {
            graph.vArray[i] = new List<int>();
        }
    }

    public bool AddEdge(int fromV, int toV)
    {
        if (!graph.vArray[fromV].Contains(toV))
        {
            graph.numE++;
            graph.vArray[fromV].Add(toV);
            graph.vArray[toV].Add(fromV);

            stage.ConnectNodes(fromV, toV);
            return true;
        }
        else { return false; }
    }

    private bool isEulerian()
    {

        return false;
    }
}
