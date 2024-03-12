using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Typer : MonoBehaviour
{
    public WordBank wordBank = null;
    public TextMeshProUGUI wordOutput = null;
    public TextMeshProUGUI wordOutput2 = null;
    public GameObject[] boxes;

    private string remainingWord = string.Empty;
    private string currentWord = string.Empty;
    private int disabledBoxIndex = 0;

    private void Start()
    {
        SetCurrentWord();
    }

    private void SetCurrentWord()
    {
        currentWord = wordBank.GetWord();
        SetRemainingWord(currentWord);
        SetRemainingWord2(currentWord);
        DisableBox();
    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        wordOutput.text = remainingWord;
    }

    private void SetRemainingWord2(string newString)
    {
        wordOutput2.text = newString;
    }

    private void DisableBox()
    {
        if (disabledBoxIndex < boxes.Length)
        {
            boxes[disabledBoxIndex].SetActive(false); // 다음 상자 비활성화
            disabledBoxIndex++;
        }
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;

            if (keysPressed.Length == 1)
                EnterLetter(keysPressed);
                EnterLetter2(keysPressed);

        }
    }

    private void EnterLetter(string typedLetter)
    {
        if (IsCorrectLetter(typedLetter))
        {
            RemoveLetter();

            if (IsWordComplete())
                SetCurrentWord();
        }

    }

    private void EnterLetter2(string typedLetter)
    {
        if (IsCorrectLetter(typedLetter))
        {
            if (IsWordComplete())
                SetCurrentWord();
        }
    }


    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
    }

    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }
}