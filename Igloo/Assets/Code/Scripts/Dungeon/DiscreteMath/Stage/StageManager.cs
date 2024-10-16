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
    [SerializeField] GameObject finalGatePrefab;

    [SerializeField] private ActionController player;

    public UiManager uiManager;

    [SerializeField] private ShadowControl shadowControl;
    [SerializeField] private GameObject RetryPanel;
    [SerializeField] private BasePanel basePanel;
    [SerializeField] private StartNode startNodeChoice;

    public int currentStage;
    private bool isPause, isDead;
    private GameObject gate;

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
        shadowControl.LevelTime(stages[currentStage].shadowTime);
    }

    private void Update()
    {
        if (!isDead)
        {
            if (Input.GetKeyDown(KeyCode.Delete))
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

    }


    public void RestartLevel()     //RetryUI 버튼에서 호출
    {
        // 맵 재설정
        shadowControl.Restart();
        stages[currentStage].Restart();
        if(gate != null)
            Destroy(gate);


        // 플레이어 위치 재설정
        player.Reposition(stages[currentStage].startPos); // rotaion도 재설정해줘야될거같으넫....? 
        player.currentHealth = player.maxHealth;

        //UI 재설정
        OffRetryUI();

        isDead = false;
    }

    private void PlayerDie()
    {
        isDead = true;
        string str = ".\n.\n.\n\n죽었습니다.";
        SetRetryUI(str);
    }

    private void SetRetryUI(string str)
    {
        shadowControl.gameObject.SetActive(false);
        basePanel.LifeUIOff();

        //GameManager.isStaticUiOpen = true;
        UiManager.isStaticUiOpen = true;
        RetryPanel.SetActive(true);
        RetryPanel.GetComponentInChildren<Text>().text = "";
        RetryPanel.GetComponentInChildren<Text>().DOText(str, 1f).SetUpdate(true);
    }

    private void OffRetryUI()
    {
        //GameManager.isStaticUiOpen = false;
        shadowControl.gameObject.SetActive(true);

        UiManager.isStaticUiOpen = false;

        RetryPanel.SetActive(false);
        basePanel.LifeUIOn();
    }

    private void StageFinish()
    {
        if(currentStage+1 >= stages.Length)
        {
            Debug.Log("컴수 던전 클리어");
            GameEventsManager.instance.playerEvents.FinishDungeon("DiscreteMath");
            shadowControl.gameObject.SetActive(false);
            CreateGate(finalGatePrefab);
        }
        else
        {
            CreateGate(gatePrefab);
        }
    }

    private void CreateGate(GameObject gatePrefab)
    {
        Vector3 pos = player.transform.position + player.transform.forward * 4.5f + Vector3.up * -1f;
        gate = Instantiate(gatePrefab, pos, player.transform.rotation);
    }

    public void NextStage()
    {
        Destroy(gate);
        startNodeChoice.InitUi();

        stages[currentStage++].gameObject.SetActive(false);
        stages[currentStage].gameObject.SetActive(true);
        shadowControl.LevelTime(stages[currentStage].shadowTime);

        //ui 세팅
        shadowControl.InitUI();
        player.Reposition(stages[currentStage].startPos);
    }

    public void SetStartPoint(int n)
    {
        stages[currentStage].SetStartNode(n);
    }

}
