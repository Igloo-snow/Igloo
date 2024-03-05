using UnityEngine;

public abstract class DungeonStepBase : MonoBehaviour
{
    public abstract void Enter();

    public abstract void Execute(DungeonManager controller);

    public abstract void Exit();
}
