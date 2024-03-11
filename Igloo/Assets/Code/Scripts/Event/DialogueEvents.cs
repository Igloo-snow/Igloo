using System;

public class DialogueEvents
{
    public event Action<int> onStartDialogue;
    public void StartDialogue(int id)
    {
        if (onStartDialogue != null)
        {
            onStartDialogue(id);
        }
    }

    public event Action<int> onFinishDialogue;
    public void FinishDialogue(int id)
    {
        if (onFinishDialogue != null)
        {
            onFinishDialogue(id);
        }
    }

    public event Action<int> onUpdateDialogueIndex;
    public void UpdateDialogueIndex(int id)
    {
        if(onUpdateDialogueIndex != null)
        {
            onUpdateDialogueIndex(id);
        }
    }
}
