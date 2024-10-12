using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateKeeperAction : AlgoNpcAction
{
    [SerializeField] int giveWeaponActionIndex;
    [SerializeField] int hiddenPathActionIndex;

    [SerializeField] Animator animator;


    [Header("GiveWeapon")]
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] Transform weaponPosition;
    private int count = 1;

    [Header("ShowHiddenPath")]
    [SerializeField] int nextSceneIndex = 17;

    protected override void ActionStart()
    {
        if(nextEventId == giveWeaponActionIndex)
        {
            if(count <= 0) { return; }
            GiveWeapon();
        }
        else if(nextEventId == hiddenPathActionIndex) 
        {
            ShowHiddenPath();
        }
    }

    private void GiveWeapon()
    {
        animator.SetTrigger("Touch");
        Instantiate(weaponPrefab, weaponPosition.position, Quaternion.identity);
        count--;
    }

    private void ShowHiddenPath()
    {
        FindAnyObjectByType<SceneMgr>().StartLoadScene(nextSceneIndex);

    }
}

