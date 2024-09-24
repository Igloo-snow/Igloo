using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlgoSpeechBubble : SpeechBubbleBase
{
    public TMP_Text speakerName;

    private void Start()
    {
        ResizeText();
    }
}
