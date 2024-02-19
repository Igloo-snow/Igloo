using System;

public class DialogueEvents
{
    public event Action<int> onFinishDialogue;
    public void FinishDialogue(int id)
    {
        if (onFinishDialogue != null)
        {
            onFinishDialogue(id);
        }
    }
}
