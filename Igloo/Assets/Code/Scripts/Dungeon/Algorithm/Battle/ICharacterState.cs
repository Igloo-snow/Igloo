using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterState
{
    int GetHealth();
    void Die();
    
}