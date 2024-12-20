using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quiz : MonoBehaviour
{
    private ActionController player;
    private BasePanel panel;
    public GameObject[] quizzes;
    public AudioClip incorrect;
    public AudioClip correct;

    private Enemy enemy;

    private int currentIndex = 0;

    private void Start()
    {
        player = FindAnyObjectByType<ActionController>();
        panel = FindAnyObjectByType<BasePanel>();
    }

    public void ShowQuiz(Enemy enemy)
    {
        this.enemy = enemy;
        quizzes[currentIndex].SetActive(true);
        UiManager.isStaticUiOpen = true;
    }

    public void WrongAnswer()
    {
        SoundManager.instance.Play(incorrect);
        player.currentHealth--;
        panel.UpdateLifeUI(player.currentHealth);
    }

    public void RightAnswer()
    {
        SoundManager.instance.Play(correct);
        StartCoroutine(AnswerCoroutine());
        
    }

    IEnumerator AnswerCoroutine()
    {
        if(quizzes[currentIndex].transform.Find("answer"))
        {
            quizzes[currentIndex].transform.Find("answer").gameObject.SetActive(true);
            Debug.Log("done");
        }

        yield return new WaitForSecondsRealtime(1f);
        Debug.Log("done1");


        quizzes[currentIndex].SetActive(false);
        if (currentIndex + 1 < quizzes.Length)
        {
            currentIndex++;
        }
        UiManager.isStaticUiOpen = false;
        enemy.ReadyToDie();

    }
}
