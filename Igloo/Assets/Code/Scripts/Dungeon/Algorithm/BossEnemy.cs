using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public GameObject weapon;

    public void WeaponOn() //WeaponOn 애니메이션에서 호출
    {
        weapon.gameObject.SetActive(true);  
    }
}
