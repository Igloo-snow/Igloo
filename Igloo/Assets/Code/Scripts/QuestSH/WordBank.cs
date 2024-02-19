using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordBank : MonoBehaviour
{
    private List<string> originalWords = new List<string>()
    {
        "printf", "hello world"
    };

    private List<string> workingWords = new List<string>();
    private int currentIndex = 0;
    public GameObject uiContainer;

    private void Awake()
    {
        workingWords.AddRange(originalWords);
        ConverToLower(workingWords);
    }

    private void ConverToLower(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
            list[i] = list[i].ToLower();
    }

    public string GetWord()
    {
        string newWord = string.Empty;

        if (currentIndex < workingWords.Count)
        {
            newWord = workingWords[currentIndex];
            currentIndex++;
        }
        else
        {
            if (uiContainer != null)
                uiContainer.SetActive(false);
        }

        return newWord;
    }
}