using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingObject : MonoBehaviour
{
    [SerializeField] private GameObject effect;
    public void StartBlinking()
    {
        StartCoroutine(ParticleCoroutine());
    }

    IEnumerator ParticleCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        effect.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        effect.SetActive(true);

        StartCoroutine(ParticleCoroutine());

    }

    public void StopBlinking()
    {
        gameObject.SetActive(false);
    }
}
