using System;

public class PlayerEvents
{
    public event Action onPlayerDie;
    public void PlayerDie()
    {
        if (onPlayerDie != null)
        {
            onPlayerDie();
        }
    }

    public event Action onPlayerStop;
    public void PlayerStop()
    {
        if (onPlayerStop != null)
        {
            onPlayerStop();
        }
    }

    public event Action onPlayerStart;
    public void PlayerStart()
    {
        if (onPlayerStart != null)
        {
            onPlayerStart();
        }
    }

    public event Action<int> onStepNode;
    public void StepNode(int i)
    {
        if (onPlayerStart != null)
        {
            onStepNode(i);
        }
    }

    public event Action onStageFinish;
    public void StageFinish()
    {
        if (onStageFinish != null)
        {
            onStageFinish();
        }
    }

    public event Action onNextStage;
    public void NextStage()
    {
        if (onNextStage != null)
        {
            onNextStage();
        }
    }
}
