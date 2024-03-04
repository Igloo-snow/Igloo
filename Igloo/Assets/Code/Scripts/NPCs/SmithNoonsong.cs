using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SmithNoonsong : MonoBehaviour
{
    private float currentTime;

    [SerializeField] private float waitTime;

    private bool isAction;

    //
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        currentTime = waitTime;
        isAction = true;
    }

    // Update is called once per frame
    void Update()
    {
        ElapseTime();
    }

    private void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
                ResetAction();
        }
    }

    private void ResetAction()
    {
        isAction = true;
        RandomAction();
    }

    private void RandomAction()
    {
        int _random = Random.Range(0, 4);

        if (_random == 0)
            Idle();
        else if (_random == 1)
            Hit();
        else if (_random == 2)
            LookAround();
        else if (_random == 3)
            MoveHand();
    }

    private void Idle()
    {
        currentTime = waitTime;
    }

    private void Hit()
    {
        currentTime = waitTime;
        anim.SetTrigger("HitDown");
    }

    private void LookAround()
    {
        currentTime = waitTime;
        anim.SetTrigger("LookAround");
    }

    private void MoveHand()
    {
        currentTime = waitTime;
        anim.SetTrigger("MoveHand");
    }
}
