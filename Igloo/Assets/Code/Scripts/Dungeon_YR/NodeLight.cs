using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLight : MonoBehaviour
{
    private Light nodeLight;
    private bool isTouched;
    [SerializeField] GameObject shadowPanel;
    private Color originColor;

    void Start()
    {
        nodeLight = GetComponent<Light>();
        originColor = nodeLight.color;
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

    public void SetColor()
    {
        nodeLight.color = originColor;
    }

}
