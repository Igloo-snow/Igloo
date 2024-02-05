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
}
