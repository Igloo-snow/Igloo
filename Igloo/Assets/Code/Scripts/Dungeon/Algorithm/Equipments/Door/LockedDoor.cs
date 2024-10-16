using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class LockedDoor : Interactable
{
    [SerializeField] Item keyItem; 

    public bool isOpen;
    private Animator animator;
    [SerializeField] bool isFinalDoor = false;

    private int finalAlgoScene = 18;

    void Start()
    {
        promptMessage = "";
        isOpen = false;
        animator = GetComponent<Animator>();
    }

    protected override void Interact()
    {
        if (!InventoryManager.Instance.CheckInventory(keyItem.id))
        {
            promptMessage = "���� ����ִ�";
            return;
        }
        else
        {
            promptMessage = "";
            InventoryManager.Instance.Remove(keyItem);
        }
        
        isOpen = !isOpen;
        if (isOpen)
        {
            animator.SetTrigger("open");
            SoundManager.instance.Play("OpenWoodDoor3");
            if (isFinalDoor)
            {
                // �� ��ȯ
                // �� �� ���� ��ٷ����ҵ�
                StartCoroutine(ChangeSceneCoroutine());        
            }

        }
    }

    IEnumerator ChangeSceneCoroutine()
    {
        yield return new WaitForSeconds(1);
        FindAnyObjectByType<SceneMgr>().StartLoadScene(finalAlgoScene);

    }
}
