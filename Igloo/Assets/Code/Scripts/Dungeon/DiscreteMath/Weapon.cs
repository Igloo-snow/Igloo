using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float rate;
    public int damage;
    public BoxCollider meleeArea;

    public void Use()
    {
        StopCoroutine("Swing");
        StartCoroutine("Swing");
    }

    IEnumerator Swing()
    {
        meleeArea.enabled = true;

        yield return new WaitForSeconds(2f);
        meleeArea.enabled = false;
    }
}
