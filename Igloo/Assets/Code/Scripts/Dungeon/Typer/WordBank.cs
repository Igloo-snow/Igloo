using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordBank : MonoBehaviour
{
    private List<string> originalWords = new List<string>()
    {
        "#include <stdio.h>", "int main(void)", "int a,b;", "int sum;", "a=10;", "b=20;", "sum=a+b;", "printf(\"sum=%d\\n\",sum);", "return 0;"
    };

    private List<string> workingWords = new List<string>();
    private int currentIndex = 0;
    public GameObject uiContainer;
    public GameObject PGBook;
    public Rigidbody rb;

    void Start()
    {
        rb = PGBook.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody ������Ʈ�� ã�� �� �����ϴ�.");
        }
        else
        {
            rb.useGravity = false;
        }

        PGBook.SetActive(false);
    }

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
            {
                if (PGBook != null)
                {
                    PGBook.SetActive(true);
                    /*Instantiate(PGBook);

                    rb = PGBook.GetComponent<Rigidbody>();
                    if (rb == null)
                    {
                        Debug.LogError("Rigidbody ������Ʈ�� ã�� �� �����ϴ�.");
                    }
                    else
                    {
                        rb.useGravity = false;
                    }*/

                    rb.useGravity = true;
                }
                UiManager.instance.OffUi(uiContainer.GetComponent<UiBase>());
            }
        }

        return newWord;
    }
}