using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
