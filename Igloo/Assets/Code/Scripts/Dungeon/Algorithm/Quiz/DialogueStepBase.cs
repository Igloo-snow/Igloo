using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DialogueStepBase : MonoBehaviour
{
    public abstract void Enter();

    public abstract void Execute(DialogueHandler handler);

    public abstract void Exit();
}
