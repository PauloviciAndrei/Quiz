using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz question", fileName = "New Question")]

public class QuestionSO : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField] string question = "Enter new question text here";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswer;

    public string getQuestion()
    {
        return question;
    }

    public int getCorrectAnswerIndex()
    {
        return correctAnswer;
    }
    
    public string getAnswer(int index)
    {
        return answers[index];
    }
}
