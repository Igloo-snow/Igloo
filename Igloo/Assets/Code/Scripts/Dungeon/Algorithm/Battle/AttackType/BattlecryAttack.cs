using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlecryAttack : MonoBehaviour, IAttackBase
{
    public GameObject noonsongGroup;

    public void StartAttack()
    {
        StartCoroutine(CryCoroutine());
    }

    public void FinishAttack()
    {
    }


    IEnumerator CryCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        SoundManager.instance.Play("BossScream", Sound.Effect, 0.5f);
        yield return new WaitForSeconds(1f);
        noonsongGroup.SetActive(true);
    }
}
