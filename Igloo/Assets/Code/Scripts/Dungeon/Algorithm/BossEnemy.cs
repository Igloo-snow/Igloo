using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public GameObject weapon;

    public void WeaponOn() //WeaponOn �ִϸ��̼ǿ��� ȣ��
    {
        weapon.gameObject.SetActive(true);  
    }
}
