using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLight : MonoBehaviour
{
    private Light nodeLight;
    private bool isTouched;
    [SerializeField] GameObject shadowPanel;

    void Start()
    {
        nodeLight = GetComponent<Light>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isTouched = true;
            nodeLight.color = Color.gray;
            shadowPanel.GetComponent<ShadowControl>().GetBrighter();
        }
    }

}
