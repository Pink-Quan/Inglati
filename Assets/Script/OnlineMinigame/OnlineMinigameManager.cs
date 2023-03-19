using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlineMinigameManager : MonoBehaviour
{
    [SerializeField] private Question[] questions;
    [SerializeField] private int numberOfQuestions=5;

    [Header("UI")]
    [SerializeField] private GameObject questionGame;
    [SerializeField] private Text questionText;
    [SerializeField] private InputField answer;

    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject congratBoard;
    [SerializeField] private Text scoreCongratText;

    int questionIndex=0;
    private int score=0;

    public virtual void StartMinigame()
    {
        ShuffleQuestions<Question>(questions); 
        questionGame.gameObject.SetActive(true);
    }

    private void ShuffleQuestions<T>(T[] arrayToShuffle)
    {
        // Create a Random object to generate random numbers
        System.Random random = new System.Random();

        // Perform the Fisher-Yates shuffle
        for (int i = arrayToShuffle.Length - 1; i > 0; i--)
        {
            // Generate a random index between 0 and i (inclusive)
            int j = random.Next(i + 1);

            // Swap the elements at indices i and j
            T temp = arrayToShuffle[i];
            arrayToShuffle[i] = arrayToShuffle[j];
            arrayToShuffle[j] = temp;
        }

        // The array is now shuffled

    }

    public virtual void StartQuestion()
    {
        if(questionIndex==questions.Length)
        {
            congratBoard.SetActive(true);
            scoreCongratText.text = score.ToString();

            return;
        }
        questionText.text = questions[questionIndex].question;
    }
    
    public virtual void AnswerQuestion()
    {
        int getScore = 0;
        if (answer.text == questions[questionIndex].answer)
            getScore = 1;
        score += getScore;
        scoreText.text = score.ToString();
        questionIndex++;
        StartQuestion();
    }

    public void BackToMap()
    {
        SceneController.instance.LoadToScene("MainMap");
    }
    public void ChangeScene(string name)
    {
        Time.timeScale = 1;
        SceneController.instance.LoadToScene(name);
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }
}

[System.Serializable]

public struct Question
{
    public string question;
    public string answer;

    public AnsMultipleChoice ansMultipleChoice;

    [System.Serializable]
    public enum AnsMultipleChoice
    {
        A=0, B=1, C=2, D=3, None=4
    }
}
