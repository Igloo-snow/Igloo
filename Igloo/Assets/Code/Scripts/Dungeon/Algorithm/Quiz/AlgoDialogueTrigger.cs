using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgoDialogueTrigger : MonoBehaviour
{
    [SerializeField] private AlgoDialogueOccasion dialogue;

    private int currentLineId = 0;

    private void Start()
    {
        GetDialogue();
        ShowDialogue(currentLineId);
    }

    public AlgoDialogue[] GetDialogue()
    {
        //모든 대사를 가져온 것
        dialogue.dialogues = AlgoDataManager.instance.GetDialogue((int)dialogue.line.x, (int)dialogue.line.y);
        
        return dialogue.dialogues;
    }

    private void ShowDialogue(int startId)
    {
        //대화창 켜기 - 어디에서 해야할지 고민이네
        AlgoSpeechBubbleManager.instance.TryStartDialogue(dialogue);

        //for (int i = startId; i < dialogue.dialogues.Length;)
        //{
        //    AlgoSpeechBubbleManager.instance.OnDialogue(dialogue.dialogues[i]);
        //    break;
        //    //이 아래 과정은 asbmanager에서 하겟습니다
        //    //근데 대화를 계속 이어나가려면 어떻게 해야하지?
        //    //if (dialogue.dialogues[i].choices.Length > 0)
        //    //{
        //    //    ShowOptions(i);
        //    //    //옵션 선택 후 다음 대사 재생되도록 
        //    //}
        //    //else
        //    //{
        //    //    // next 버튼 활성화 후 다음 대사 재생되도록
        //    //}
        //}
    }

}
