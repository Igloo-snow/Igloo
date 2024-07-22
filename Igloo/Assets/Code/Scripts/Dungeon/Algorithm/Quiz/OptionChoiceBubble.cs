using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OptionChoiceBubble : MonoBehaviour
{
    public TMP_Text text;

    public AlgoChoiceOption option;


    public void Chosen()
    {
        AlgoSpeechBubbleManager.instance.ShowOptionChosen(option);
        
    }
}
