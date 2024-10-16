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
            promptMessage = "문이 잠겨있다";
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
                // 씬 전환
                // 몇 초 정도 기다려야할듯
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
