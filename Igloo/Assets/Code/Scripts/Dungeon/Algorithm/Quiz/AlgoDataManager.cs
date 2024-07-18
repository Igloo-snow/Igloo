using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgoDataManager : MonoBehaviour
{
    public static AlgoDataManager instance;

    [SerializeField] private string csv_FileName;

    Dictionary<int, AlgoDialogue> dialogueDic = new Dictionary<int, AlgoDialogue>();

    public static bool isFinishl = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            AlgoDialogueParser theParser = GetComponent<AlgoDialogueParser>();
            AlgoDialogue[] dialogues = theParser.Parse(csv_FileName);
            for(int i = 0; i < dialogues.Length; i++)
            {
                dialogueDic.Add(i+1, dialogues[i]);
            }
            isFinishl = true;
        }
    }

    public AlgoDialogue[] GetDialogue(int startNum, int endNum)
    {
        List<AlgoDialogue> dialogueList = new List<AlgoDialogue>();

        for(int i = 0; i<= endNum - startNum; i++)
        {
            dialogueList.Add(dialogueDic[i +1]);
        }

        return dialogueList.ToArray();
    }
}
