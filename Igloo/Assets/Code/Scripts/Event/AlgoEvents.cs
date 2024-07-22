using System;
using UnityEngine.AI;

public class AlgoEvents
{
    public event Action<int> onAlgoQuizRight;
    public void AlgoQuizRight(int id)
    {
        if (onAlgoQuizRight != null)
        {
            onAlgoQuizRight(id);
        }
    }
}
