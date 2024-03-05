using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplanationOR : Interactable
{
    public bool isPressed;

    void Start()
    {
        promptMessage = "[E] 눌러서 OR Gate 설명 보기";
        isPressed = false;
    }

    void Update()
    {

    }

    protected override void Interact()
    {
        isPressed = true;
        promptMessage = "입력 중 하나만 True여도 결과가 True가 된다.";
    }
}
