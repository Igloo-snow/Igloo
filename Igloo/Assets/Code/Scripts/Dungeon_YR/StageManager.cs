using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class GM : MonoBehaviour
{
    [SerializeField] private StageGraph[] stages;
    [SerializeField] GameObject gatePrefab;

    [SerializeField] private ActionController player;
    [SerializeField] private ShadowControl shadowControl;
    [SerializeField] private GameObject RetryPanel;

    public int currentStage;
    private bool isPause, isDead;

    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onPlayerDie += PlayerDie;
        GameEventsManager.instance.playerEvents.onStageFinish += StageFinish;
        GameEventsManager.instance.playerEvents.onNextStage += NextStage;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.playerEvents.onPlayerDie -= PlayerDie;
        GameEventsManager.instance.playerEvents.onStageFinish -= StageFinish;
        GameEventsManager.instance.playerEvents.onNextStage -= NextStage;
    }

    private void Start()
    {

    }
    private void Update()
    {
        if (!isDead)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPause)
                {
                    isPause = false;
                    OffRetryUI();
                }
                else
                {
                    isPause = true;
                    string str = "다시\n시도하시겠습니까?";
                    SetRetryUI(str);
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.G))
            CreateGate();


    }

    public void ReStartLevel()
    {
        // 맵 재설정
        shadowControl.Restart();
        Debug.Log(currentStage);
        stages[currentStage].ResetFallingObj();
        stages[currentStage].ResetColor();
        stages[currentStage].previousNode = -1;


        // 플레이어 위치 재설정
        player.Reposition(stages[currentStage].startPos); // rotaion도 재설정해줘야될거같으넫....? 
        Debug.Log(stages[currentStage].startPos);

        //UI 재설정
        OffRetryUI();

        Time.timeScale = 1.0f;
        isDead = false;
    }

    private void PlayerDie()
    {
        isDead = true;
        //retry ui 설정
        string str = ".\n.\n.\n\n죽었습니다.";
        SetRetryUI(str);
    }

    private void SetRetryUI(string str)
    {
        GameManager.isOpenRestartUI = true;
        RetryPanel.GetComponentInChildren<Text>().text = "";
        RetryPanel.GetComponentInChildren<Text>().DOText(str, 1f).SetUpdate(true);

        RetryPanel.SetActive(true);
        shadowControl.gameObject.SetActive(false);
    }

    private void OffRetryUI()
    {
        GameManager.isOpenRestartUI = false;
        RetryPanel.SetActive(false);
        shadowControl.gameObject.SetActive(true);
    }


    private void StageFinish()
    {
        if(currentStage+1 >= stages.Length)
        {
            Debug.Log("컴수 던전 클리어");
        }
        else
        {
            CreateGate();

            //stages[currentStage++].gameObject.SetActive(false);
            //stages[currentStage].gameObject.SetActive(true);

            //ui 세팅
            //shadowControl.InitUI();
            //player.Reposition(stages[currentStage].startPos);
        }
    }

    private void CreateGate()
    {
        Vector3 pos = player.transform.position + player.transform.forward * 4.0f + Vector3.up * -2f;
        var gate = Instantiate(gatePrefab, pos, player.transform.rotation);

        Vector3 rotationDirection = (pos - player.transform.position);
        gate.transform.forward = rotationDirection;
    }

    public void NextStage()
    {
        stages[currentStage++].gameObject.SetActive(false);
        stages[currentStage].gameObject.SetActive(true);

        //ui 세팅
        shadowControl.InitUI();
        player.Reposition(stages[currentStage].startPos);
    }
}
