using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
   public List<QuestionAnswer> QnA;
   public GameObject[] options;
   public int currentQuestion;

   public GameObject Quizpanel;
   public GameObject GoPanel;

   public Text QuestionTxt;
   public Text ScoreTxt;

   int totalQuestions = 0;
   public int score;

   private void Start()
   {
    totalQuestions = QnA.Count;
    GoPanel.SetActive(false);
    generateQuestion();
   }

   public void Retry()
   {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }

   public void GameOver()
   {
    Quizpanel.SetActive(false);
    GoPanel.SetActive(true);
    ScoreTxt.text = score + "/" + totalQuestions;
    }

   public void Correct()
   {
    score += 1;
    QnA.RemoveAt(currentQuestion);
    generateQuestion();
   }

   public void Wrong()
   {
    //when answer wrong
    QnA.RemoveAt(currentQuestion);
    generateQuestion();
   }

   void SetAnswer()
   {
    for (int i = 0; i < options.Length; i++)
    {
        options[i].GetComponent<AnswerScript>().isCorrect = false;
        options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answer[i];

        if(QnA[currentQuestion].CorrectAnswer == i+1)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = true;
        }
    }
   }
   
   void generateQuestion()
   {
    if(QnA.Count > 0)
    {
        currentQuestion = Random.Range(0, QnA.Count);

        QuestionTxt.text = QnA[currentQuestion].Question;
        SetAnswer();
    }
    else
    {
        Debug.Log("Out of Question");
        GameOver();
    }
    

   
   }
}
