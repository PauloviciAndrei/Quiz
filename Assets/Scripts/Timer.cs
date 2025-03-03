using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToShowCorrectAnswer = 10f;
    [SerializeField] float timeToCompleteQuestion = 30f;

    public bool loadNextQuestion = true;
    public bool isAnsweringQuestion = false;
    public float fillFraction;

    private void Awake()
    {
        UpdateTimer();
    }

    float timerValue;
    void Update()
    {   
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    private void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
        if(timerValue <= 0)
            if(isAnsweringQuestion)
            {
                timerValue = timeToCompleteQuestion;
                isAnsweringQuestion = false;
            }
            else
            {
                timerValue = timeToShowCorrectAnswer;
                isAnsweringQuestion = true;
                loadNextQuestion = true;
            }
        else
        {   
            if(isAnsweringQuestion)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else
            {
                fillFraction = timerValue / timeToCompleteQuestion;
            }
        }
    }
}
