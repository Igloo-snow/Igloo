using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfNpc : Npc
{
    private void Start()
    {
        anim.SetBool("IsThinking", true);
    }
}
