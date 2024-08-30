using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropPLT : MonoBehaviour
{
    Vector3 offset;
    Vector3 originPos;
    public string destinationTag = "DropArea";
    public string destinationName = "";
    bool isHit;
    bool answer;

    public GameObject quest1;

    public bool getAnswer()
    {
        return answer;
    }

    void Start()
    {
        originPos = transform.position;
        isHit = false;
        quest1 = transform.parent.gameObject;
    }

    void OnMouseDown()
    {
        offset = transform.position - MouseWorldPosition();
        transform.GetComponent<Collider>().enabled = false;
    }

    void OnMouseDrag()
    {
        transform.position = MouseWorldPosition() + offset;
    }

    void OnMouseUp()
    {
        var rayOrigin = Camera.main.transform.position;
        var rayDirection = MouseWorldPosition() - Camera.main.transform.position;
        RaycastHit hitInfo;

        isHit = false;
        if (Physics.Raycast(rayOrigin, rayDirection, out hitInfo))
        {
            if (hitInfo.transform.tag == destinationTag)
            {
                transform.position = hitInfo.transform.position;
                isHit = true;

                if (hitInfo.transform.name == destinationName)
                {
                    answer = true;
                    quest1.GetComponent<PLTQuest1>().CheckRoots();
                }
                else
                    answer = false;
            }
        }
        transform.GetComponent<Collider>().enabled = true;

        if (!isHit)
        {
            transform.position = originPos;
        }
    }

    Vector3 MouseWorldPosition()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}
