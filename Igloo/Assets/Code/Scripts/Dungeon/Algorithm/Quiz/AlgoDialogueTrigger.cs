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
        //��� ��縦 ������ ��
        dialogue.dialogues = AlgoDataManager.instance.GetDialogue((int)dialogue.line.x, (int)dialogue.line.y);
        
        return dialogue.dialogues;
    }

    private void ShowDialogue(int startId)
    {
        //��ȭâ �ѱ� - ��𿡼� �ؾ����� ����̳�
        AlgoSpeechBubbleManager.instance.TryStartDialogue(dialogue);

        //for (int i = startId; i < dialogue.dialogues.Length;)
        //{
        //    AlgoSpeechBubbleManager.instance.OnDialogue(dialogue.dialogues[i]);
        //    break;
        //    //�� �Ʒ� ������ asbmanager���� �ϰٽ��ϴ�
        //    //�ٵ� ��ȭ�� ��� �̾������ ��� �ؾ�����?
        //    //if (dialogue.dialogues[i].choices.Length > 0)
        //    //{
        //    //    ShowOptions(i);
        //    //    //�ɼ� ���� �� ���� ��� ����ǵ��� 
        //    //}
        //    //else
        //    //{
        //    //    // next ��ư Ȱ��ȭ �� ���� ��� ����ǵ���
        //    //}
        //}
    }

}
