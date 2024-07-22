using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AlgoDialogue
{
    [Tooltip("대화 ID")]
    public int id;

    [Tooltip("대사 치는 캐릭터 이름")]
    public string name;

    [Tooltip("대사 내용")]
    public string[] contexts;

    public AlgoChoiceOption[] choices;

    public bool isFinal;

    [Tooltip("연관 이벤트 존재 유무")]
    public bool nextEvent;
}

[System.Serializable]
public class AlgoDialogueOccasion
{
    public string name;

    public Vector2 line;
    public AlgoDialogue[] dialogues;
}
