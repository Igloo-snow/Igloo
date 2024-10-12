using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAttack : MonoBehaviour, IAttackBase
{
    public TrailRenderer trail;

    public void StartAttack()
    {
        trail.emitting = true;
        SoundManager.instance.Play("blade", Sound.Effect, 0.3f);
    }

    public void FinishAttack()
    {
        trail.emitting = false;
    }

}
