using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AlgoDialogue
{
    [Tooltip("��ȭ ID")]
    public int id;

    [Tooltip("��� ġ�� ĳ���� �̸�")]
    public string name;

    [Tooltip("��� ����")]
    public string[] contexts;

    public AlgoChoiceOption[] choices;
}

[System.Serializable]
public class AlgoChoiceOption
{
    [Tooltip("��ȭ ID")]
    public int id;

    [Tooltip("�ɼ� ����")]
    public string option;

    [Tooltip("���� ��� ID")]
    public int nextId;

}

[System.Serializable]
public class AlgoDialogueOccasion
{
    public string name;

    public Vector2 line;
    public AlgoDialogue[] dialogues;
}
