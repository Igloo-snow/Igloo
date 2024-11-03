using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AGLever : Interactable
{
    Animator animator;
    [SerializeField] TMP_Text text;
    [SerializeField] ParticleSystem fx;

    [SerializeField] Animator portAnim; // öâ
    [SerializeField] BubbleNpcV2 relatedNpc; // öâ �� npc

    private void Start()
    {
        animator = GetComponent<Animator>();
        promptMessage = "E:������ ������";
    }
    protected override void Interact()
    {
        fx.Stop();
        animator.SetTrigger("PowerBoxOn");
        portAnim.SetTrigger("PortcullisUp");
    }

    public void LeverUp() //PowerBoxOn�ִϸ��̼ǿ��� ȣ��
    {
        promptMessage = "";
        text.text = "����� ���� ���ȴ�";
        relatedNpc.LeverUp();
    }
}
