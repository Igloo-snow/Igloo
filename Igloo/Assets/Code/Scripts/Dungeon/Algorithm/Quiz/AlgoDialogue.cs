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
}

[System.Serializable]
public class AlgoChoiceOption
{
    [Tooltip("대화 ID")]
    public int id;

    [Tooltip("옵션 내용")]
    public string option;

    [Tooltip("다음 대사 ID")]
    public int nextId;

}

[System.Serializable]
public class AlgoDialogueOccasion
{
    public string name;

    public Vector2 line;
    public AlgoDialogue[] dialogues;
}
