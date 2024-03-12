using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClick : MonoBehaviour, IPointerClickHandler
{
    public int slotNum;
    public GameObject deleteBtn;
    public TMP_Text deleteText;
    public TMP_Text playerName;
    private SelectUi selectUi;

    private void Start()
    {
        selectUi = GetComponentInParent<SelectUi>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(playerName.text != "�������") 
            {
                deleteText.text = playerName.text + " �� �����Ͻðڽ��ϱ�?";
                deleteBtn.SetActive(true);
            }

        }
        else if (eventData.button == PointerEventData.InputButton.Left)
        {
            selectUi.Slot(slotNum);
        }
    }
}
