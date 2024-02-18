using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvents
{
    public event Action<string> onFinishDialogue;
    public void FinishDialogue(string str)
    {
        if (onFinishDialogue != null)
        {
            onFinishDialogue(str);
        }
    }
}
