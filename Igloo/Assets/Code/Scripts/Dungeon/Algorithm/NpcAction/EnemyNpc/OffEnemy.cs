using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class OffEnemy : Interactable
{
    [SerializeField] private Item item;
    [SerializeField] private ParticleSystem offParticle;
    [SerializeField] private ParticleSystem baseParticle;
    [SerializeField] private EnemyNs baseNs;
    [SerializeField] private BoxCollider collider;
    [SerializeField] private GameObject baseModel;

    private bool canGive = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(InventoryManager.Instance.CheckInventory(item.id))
            {
                canGive = true;
                promptMessage = "지친 눈송이에게 도넛을 준다:E";
            }
        }
    }

    protected override void Interact()
    {
        if (!canGive) return;

        InventoryManager.Instance.Remove(item);
        promptMessage = "";
        StartCoroutine(ClearNSCoroutine());
    }

    IEnumerator ClearNSCoroutine()
    {
        baseNs.ResetSpeed();
        collider.enabled = false;
        baseParticle.gameObject.SetActive(false);
        offParticle.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        baseModel.SetActive(false);
        SoundManager.instance.Play("46_Poison_01");
    }

}
