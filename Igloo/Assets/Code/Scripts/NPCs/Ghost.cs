using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ghost : MonoBehaviour
{
    [SerializeField] private float walkSpeed; // 걷기 스피드
    private Vector3 direction; // 방향

    private bool isAction; 

    [SerializeField] private float waitTime; // 대기 시간.
    private float currentTime;

    private Animator anim;
    private CharacterController cc;
    private AudioSource audioSource;
    [SerializeField] AudioClip crying;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
        currentTime = waitTime;
        isAction = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Move();
        //Rotation();
        ElapseTime();
    }

    private void Move()
    {
        int randomX = Random.Range(-14, 16);
        int randomY = Random.Range(5, 16);
        int randomZ = Random.Range(-17, 12);

        Vector3 diff = new Vector3 (randomX, randomY, randomZ);
            //Vector3 diff = transform.position + (transform.forward * walkSpeed);
        //transform.position = Vector3.Lerp(transform.position, diff, 0.5f);
        transform.DOMove(diff, 3f);
    }

    private void Rotation()
    {
        int randomRotationY = Random.Range(0, 360);
        Vector3 _rotation = new Vector3(0, randomRotationY, 0);
        transform.DORotate(_rotation, 1f);
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
        Rotation();
        isAction = true;
        direction.Set(0f, Random.Range(0f, 360f), 0f);
        RandomAction();
    }

    private void RandomAction()
    {
        int _random = Random.Range(0, 3);

        if (_random == 0)
            Wait();
        else if (_random == 1)
            PeekandWalk();
        else if (_random == 2)
            TryWalk();
    }

    private void Wait()
    {
        currentTime = waitTime;
        audioSource.PlayOneShot(crying);
    }

    private void PeekandWalk()
    {
        currentTime = waitTime;
        Move();
        anim.SetTrigger("Gesture");
    }

    private void TryWalk()
    {
        currentTime = waitTime;
        Move();
    }

}
