using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ActionController : MonoBehaviour
{
    private CharacterController characterController;
    private CinemachineFreeLook freeLook;
    private Animator anim;
    private Weapon weapon;

    public bool stopMoving = false;
    public bool encountered = false;
    public bool changingScene;
    public string nextScene;

    public int maxHealth = 3;
    public int currentHealth = 3;
    private float attackDelay;

    private bool isAttackReady = true;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        freeLook = FindObjectOfType<CinemachineFreeLook>();

        weapon = GetComponentInChildren<Weapon>();
        weapon.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onPlayerStop += PlayerStop;
        GameEventsManager.instance.playerEvents.onPlayerStart += PlayerStart;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onPlayerStop -= PlayerStop;
        GameEventsManager.instance.playerEvents.onPlayerStart -= PlayerStart;
    }

    void Update()
    {
        /*
        if (QuestUI.isCheckingQuest || DialogueManager.GetInstance().isPlaying)
        {
            freeLook.enabled = false;
            GetComponent<CharacterController>().enabled = false;
        }
        else
        {
            freeLook.enabled = true;
            GetComponent<CharacterController>().enabled = true;
        }
        */

        if(encountered == true && Input.GetKeyDown(KeyCode.E))
        {
            changingScene = true;
        }
        else
        {
            changingScene = false;
        }
        
        Attack();
    }

    void Attack()
    {
        attackDelay += Time.deltaTime;
        if (!weapon.isActiveAndEnabled)
            return;

        if (Input.GetMouseButtonDown(0) && !stopMoving && weapon)
        {
            isAttackReady = weapon.rate < attackDelay;

            if (isAttackReady)
            {
                weapon.Use();
                anim.SetTrigger("Attack");
                attackDelay = 0;
            }
        }
    }

    public void WeaponOn()
    {
        weapon.gameObject.SetActive(true);
    }

    private void PlayerStop()
    {
        stopMoving = true;
        Time.timeScale = 0f;
    }

    private void PlayerStart()
    {
        stopMoving = false;
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("TransitionPoint"))
        {
            encountered = true;
            nextScene = other.gameObject.GetComponent<TransitionPoint>().nextScene;
        }
        else if (other.transform.CompareTag("Enemy"))
        {
            Debug.Log("플레이어가 공격당했습니다");
            currentHealth--;
            FindObjectOfType<BasePanel>().UpdateLifeUI(currentHealth);

            if (currentHealth <= 0)
            {
                StartCoroutine(OnDie());
            }
            
        }
    }

    IEnumerator OnDie()
    {
        characterController.enabled = false;
        Debug.Log("플레이어가 사망하였습니다");
        yield return new WaitForSeconds(0.1f);
        anim.SetTrigger("Die");
        yield return new WaitForSeconds(3f);
        GameEventsManager.instance.playerEvents.PlayerDie();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("TransitionPoint"))
        {
            encountered = false;
        }
    }

    public TransitionPoint GetInteractableObject()
    {
        float interactRange = 2f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
        foreach(Collider collider in colliderArray)
        {
            if(collider.TryGetComponent(out TransitionPoint transitionInteractable)){
                return transitionInteractable;
            }
        }
        return null;
    }

    public void Reposition(GameObject pos)
    {
        anim.Play("Idle");
        GetComponent<CharacterController>().enabled = false;
        transform.position = pos.transform.position;
        transform.rotation = pos.transform .rotation;
        GetComponent<CharacterController>().enabled = true;
    }
}
