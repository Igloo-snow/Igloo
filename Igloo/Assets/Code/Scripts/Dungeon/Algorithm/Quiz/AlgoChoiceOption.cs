using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
