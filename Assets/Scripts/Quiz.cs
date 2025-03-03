using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SocialPlatforms;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    QuestionSO currentQuestion;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    [SerializeField] TextMeshProUGUI questionText;
    

    [Header("Answers")]
    int correctAnswerIndex;
    bool hasAnswerdEarly;
    [SerializeField] GameObject[] answerButtons;

    [Header("Buttons")]
    [SerializeField] Sprite defaulAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;
    public bool isComplete;


    void Awake()
    {   
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void Update()
    {
       UpdateTimer();
    }
    private void UpdateTimer()
    {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion)
        {
            hasAnswerdEarly = false;
            timer.loadNextQuestion = false;
            if (progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }
            GetNextQuestion();
            RemoveQuestion();
        }
        else
            if(!hasAnswerdEarly && !timer.isAnsweringQuestion)
            {
                DisplayAnswer(-1);
                SetButtonState(false);
            }
    }

    void GetNextQuestion()
    {   
        if(questions.Count != 0)
        {   
            scoreKeeper.IncrementSeenQuestions();
            SetButtonState(true);
            SetDefaultButtonSprite();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
        }
    }

    void GetRandomQuestion()
    {
        int index = UnityEngine.Random.Range(0, questions.Count);
        currentQuestion = questions[index];
    }

    private void RemoveQuestion()
    {
        Debug.Log("Current: " + questions.Count + "\n");
        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
        Debug.Log("Next: " + questions.Count + "\n");
    }

    private void SetDefaultButtonSprite()
    {   
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaulAnswerSprite;
        }
    }

    private void DisplayQuestion()
    {
        questionText.text = currentQuestion.getQuestion();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.getAnswer(i);
        }
    }

    public void onAnswerSelected(int index)
    {
        hasAnswerdEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Socre: " + scoreKeeper.CalculateScore() + "%";
    }

    private void DisplayAnswer(int index)
    {
        correctAnswerIndex = currentQuestion.getCorrectAnswerIndex();
        if (index == currentQuestion.getCorrectAnswerIndex())
        {   
            scoreKeeper.IncrementCorrectAnswers();
            questionText.text = "Correct!";
            Image buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
        else
        {
            questionText.text = "The correct answer:\n" + currentQuestion.getAnswer(correctAnswerIndex);
            Image buttonImage = answerButtons[correctAnswerIndex].GetComponentInChildren<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void SetButtonState(bool state)
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
}
