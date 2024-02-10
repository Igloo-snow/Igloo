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
                    string str = "�ٽ�\n�õ��Ͻðڽ��ϱ�?";
                    SetRetryUI(str);
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.G))
            CreateGate();


    }

    public void ReStartLevel()
    {
        // �� �缳��
        shadowControl.Restart();
        Debug.Log(currentStage);
        stages[currentStage].ResetFallingObj();
        stages[currentStage].ResetColor();
        stages[currentStage].previousNode = -1;


        // �÷��̾� ��ġ �缳��
        player.Reposition(stages[currentStage].startPos); // rotaion�� �缳������ߵɰŰ�����....? 
        Debug.Log(stages[currentStage].startPos);

        //UI �缳��
        OffRetryUI();

        Time.timeScale = 1.0f;
        isDead = false;
    }

    private void PlayerDie()
    {
        isDead = true;
        //retry ui ����
        string str = ".\n.\n.\n\n�׾����ϴ�.";
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
            Debug.Log("�ļ� ���� Ŭ����");
        }
        else
        {
            CreateGate();

            //stages[currentStage++].gameObject.SetActive(false);
            //stages[currentStage].gameObject.SetActive(true);

            //ui ����
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

        //ui ����
        shadowControl.InitUI();
        player.Reposition(stages[currentStage].startPos);
    }
}
