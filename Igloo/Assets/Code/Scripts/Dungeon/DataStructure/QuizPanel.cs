using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizPanel : MonoBehaviour
{
    public Button[] answerButtons;
    public bool[] isCorrectAnswers;
    public QuizManager quizManager;
    public int quizIndex;
    public Button hintButton;
    public TextMeshProUGUI hintText;

    private bool isHintVisible = false;

    void Start()
    {
        quizManager = GetComponentInParent<QuizManager>();
        quizIndex = quizManager.currentQuizIndex;

        hintText.gameObject.SetActive(false);
        hintButton.onClick.AddListener(ToggleHint);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            answerButtons[index].onClick.AddListener(() => {
                Debug.Log($"Button {index} clicked");
                OnAnswerButtonClicked(index);
            });
        }
    }

    public void OnAnswerButtonClicked(int buttonIndex)
    {
        if (quizIndex < 0 || quizIndex >= isCorrectAnswers.Length)
        {
            Debug.LogError($"Invalid quiz index: {quizIndex}");
            return; // 유효하지 않은 인덱스일 경우 종료
        }

        quizManager.OnAnswerSelected(isCorrectAnswers[buttonIndex], quizIndex);
    }

    public void ToggleHint()
    {
        isHintVisible = !isHintVisible;
        hintText.gameObject.SetActive(true);
        Debug.Log($"Hint is now {(isHintVisible ? "visible" : "hidden")}");
    }
}