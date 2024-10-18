using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerInFight : MonoBehaviour, ICharacterState
{
    [SerializeField] HealthUI healthUI;
    Animator animator;
    [SerializeField] Image UIImage;
    [Header("State")]
    private int health = 100;

    [Header("StartSetting")]
    [SerializeField] GameObject startPos;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Melee"))
        {

            healthUI.decreaseHealth(50);
            GetComponent<PlayerMovement>().Attacked(other.transform);
            SoundManager.instance.Play("61_Hit_03", Sound.Effect2, 1, 0.2f);
            
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public void Die()
    {
        GameEventsManager.instance.algoEvents.AlgoFinalBattleFinish();
        StartCoroutine(DieCoroutine());
    }

    IEnumerator DieCoroutine() // 플레이어 애니메이션, UI, 위치 조정
    {
        animator.SetTrigger("Die");
        UIImage.GetComponent<Animator>().SetTrigger("FadeOut");

        yield return new WaitForSeconds(3f);
        //플레이어 위치 및 화면 fadeOut/In
        GetComponent<ActionController>().Reposition(startPos);
        GameEventsManager.instance.algoEvents.AlgoFinalBattleInit();
        UIImage.GetComponent<Animator>().SetTrigger("FadeIn");
        

        yield return new WaitForSeconds(1f);
        animator.SetTrigger("Restart");
    }

}
