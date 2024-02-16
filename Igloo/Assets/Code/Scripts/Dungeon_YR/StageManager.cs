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
    [SerializeField] private BasePanel basePanel;

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
                    string str = "�ٽ�\n�õ��Ͻðڽ��ϱ�?";
                    SetRetryUI(str);
                }
            }
        }

        //Gate Test �ڵ�
        if(Input.GetKeyDown(KeyCode.G))
            CreateGate();
    }


    public void RestartLevel()     //RetryUI ��ư���� ȣ��
    {
        // �� �缳��
        shadowControl.Restart();
        stages[currentStage].Restart();
        if(gate != null)
            Destroy(gate);


        // �÷��̾� ��ġ �缳��
        player.Reposition(stages[currentStage].startPos); // rotaion�� �缳������ߵɰŰ�����....? 
        player.currentHealth = player.maxHealth;

        //UI �缳��
        OffRetryUI();

        Time.timeScale = 1.0f;
        isDead = false;
    }

    private void PlayerDie()
    {
        isDead = true;
        string str = ".\n.\n.\n\n�׾����ϴ�.";
        SetRetryUI(str);
    }

    private void SetRetryUI(string str)
    {
        shadowControl.gameObject.SetActive(false);
        basePanel.LifeUIOff();

        GameManager.isOpenRestartUI = true;
        RetryPanel.SetActive(true);
        RetryPanel.GetComponentInChildren<Text>().text = "";
        RetryPanel.GetComponentInChildren<Text>().DOText(str, 1f).SetUpdate(true);
    }

    private void OffRetryUI()
    {
        GameManager.isOpenRestartUI = false;
        RetryPanel.SetActive(false);
        shadowControl.gameObject.SetActive(true);
        basePanel.LifeUIOn();
    }

    private void StageFinish()
    {
        if(currentStage+1 >= stages.Length)
        {
            Debug.Log("�ļ� ���� Ŭ����");
            shadowControl.gameObject.SetActive(false);
        }
        else
        {
            CreateGate();
        }
    }

    private void CreateGate()
    {
        Vector3 pos = player.transform.position + player.transform.forward * 4.5f + Vector3.up * -1f;
        gate = Instantiate(gatePrefab, pos, player.transform.rotation);
    }

    public void NextStage()
    {
        Destroy(gate);
        stages[currentStage++].gameObject.SetActive(false);
        stages[currentStage].gameObject.SetActive(true);
        shadowControl.LevelTime(stages[currentStage].shadowTime);

        //ui ����
        shadowControl.InitUI();
        player.Reposition(stages[currentStage].startPos);
    }

}
