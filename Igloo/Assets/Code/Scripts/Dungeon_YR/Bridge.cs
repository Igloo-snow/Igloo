using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    [SerializeField] private GameObject platformPrefab;
    private GameObject[] platforms;
    [SerializeField] private Transform parent; //������ �����ϱ�� �ص� �̰� �ǳ� �𸣰پ�

    private float extents;
    private int num;

    // Start is called before the first frame update
    void Awake()
    {
        extents = platformPrefab.GetComponent<BoxCollider>().bounds.extents.x *2;
        num = (int)(this.transform.localScale.x / extents);
        Debug.Log(extents + this.gameObject.name + " " + num);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
