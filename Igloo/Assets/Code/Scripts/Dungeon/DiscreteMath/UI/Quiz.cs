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
        GameManager.isStaticUiOpen = true;
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
        quizzes[currentIndex].SetActive(false);
        if(currentIndex+1 < quizzes.Length)
        {
            currentIndex++;
        }
        GameManager.isStaticUiOpen = false;
        enemy.ReadyToDie();
    }
}
