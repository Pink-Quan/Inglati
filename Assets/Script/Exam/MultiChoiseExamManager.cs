using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiChoiseExamManager : MonoBehaviour
{
    [SerializeField] protected Question[] questions;

    [SerializeField] protected GameObject congratBoard;
    [SerializeField] protected Text scoreCongratText;

    [SerializeField] protected MultiChoiseQuest questPrefab;
    [SerializeField] protected Transform questContainer;

    protected List<MultiChoiseQuest> questArr=new List<MultiChoiseQuest>();
    protected int score = 0;

    private void Start()
    {
        ShuffleArray<Question>(questions);
        SpawnQuest();
    }

    public virtual void SpawnQuest()
    {
        for (int i = 0; i < questions.Length; i++)
        {
            var t = Instantiate(questPrefab);
            t.transform.SetParent(questContainer,false);
            t.SetQuestion(questions[i]);
            questArr.Add(t);
        }
    }

    public virtual void Submit()
    {
        foreach(var t in questArr)
        {
            if (t.IsRight())
                score++;
        }
        congratBoard.SetActive(true);
        scoreCongratText.text = score.ToString();
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
    private void ShuffleArray<T>(T[] arrayToShuffle)
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
}
