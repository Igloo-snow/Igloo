using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AGLever : Interactable
{
    Animator animator;
    [SerializeField] TMP_Text text;
    [SerializeField] ParticleSystem fx;

    [SerializeField] Animator portAnim; // 철창
    [SerializeField] BubbleNpcV2 relatedNpc; // 철창 앞 npc

    private void Start()
    {
        animator = GetComponent<Animator>();
        promptMessage = "E:레버를 내린다";
    }
    protected override void Interact()
    {
        fx.Stop();
        animator.SetTrigger("PowerBoxOn");
        portAnim.SetTrigger("PortcullisUp");
    }

    public void LeverUp() //PowerBoxOn애니메이션에서 호출
    {
        promptMessage = "";
        text.text = "어딘가의 문이 열렸다";
        relatedNpc.LeverUp();
    }
}
