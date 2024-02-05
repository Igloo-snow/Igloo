using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform fallingObjparent;
    [SerializeField] private ActionController player;
    [SerializeField] private ShadowControl shadowControl;
    [SerializeField] private GameObject RetryPanel;
    private GameObject[] fallingObjs;
    private Vector3 startPos;

    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onPlayerDie += PlayerDie;
    }

    // Start is called before the first frame update
    void Start()
    {
        fallingObjs = new GameObject[fallingObjparent.childCount];
        for(int i = 0; i < fallingObjparent.childCount; i++)
        {
            fallingObjs[i] = fallingObjparent.GetChild(i).gameObject;
        }
        startPos = player.transform.localPosition;
    }

    public void ReStartLevel()
    {
        // �� �缳��
        shadowControl.Restart();
        for (int i = 0; i < fallingObjs.Length; i++)
        {
            fallingObjs[i].gameObject.SetActive(true);

            fallingObjs[i].GetComponent<FallingObject>().Rearrange();
        }

        // �÷��̾� ��ġ �缳��
        
        player.Reposition(startPos); // rotaion�� �缳������ߵɰŰ�����....? 

        //UI �缳��
        RetryPanel.SetActive(false);
        shadowControl.gameObject.SetActive(true);

        Time.timeScale = 1.0f;
    }

    private void PlayerDie()
    {
        //retry ui ����
        RetryPanel.SetActive(true);
        shadowControl.gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

}
