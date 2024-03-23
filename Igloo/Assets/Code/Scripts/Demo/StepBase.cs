using UnityEngine;

public abstract class StepBase : MonoBehaviour
{
    protected bool isCompleted = false;
    protected bool isNext = false;

    public abstract void Enter();

    public abstract void Execute(StepController controller);

    public abstract void Exit();
}
