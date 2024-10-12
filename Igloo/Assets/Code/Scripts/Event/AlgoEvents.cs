using System;
using UnityEngine.AI;

public class AlgoEvents
{
    public event Action<int, int> onAlgoQuizRight;
    public void AlgoQuizRight(int id , int eventId = 0)
    {
        if (onAlgoQuizRight != null)
        {
            onAlgoQuizRight(id, eventId);
        }
    }
}
