using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AlgoController : MonoBehaviour
{
    private ActionController player;
    [SerializeField] private GameObject startPos;

    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onPlayerDie += PlayerDie;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onPlayerDie -= PlayerDie;
    }

    private void Start()
    {
        player = FindObjectOfType<ActionController>();
    }

    private void PlayerDie()
    {
        player.Reposition(startPos);
    }
}
