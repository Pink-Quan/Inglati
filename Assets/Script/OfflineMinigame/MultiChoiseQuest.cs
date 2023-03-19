using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiChoiseQuest : MonoBehaviour
{
    [SerializeField] private Text questionText;
    [SerializeField] private Button[] allButAns;

    public Question questionData;
    Question.AnsMultipleChoice userAns = Question.AnsMultipleChoice.None;

    private void Start()
    {
        //for(int i=0;i<allButAns.Length;i++)
        //{
        //    int t = i;
        //    allButAns[i].onClick.AddListener(()=>SetAnswer(t));
        //}
    }

    public void SetQuestion(Question qs)
    {
        questionData = qs;
        questionText.text = qs.question;
    }

    public void SetAnswer(int ans)
    {
        userAns = (Question.AnsMultipleChoice)ans;
        for (int i = 0; i < allButAns.Length; i++)
        {
            allButAns[i].image.color = Color.white;
        }
    }

    public void SetButtonColor(Button but)
    {
        but.image.color = Color.yellow;
    }

    public bool IsRight()
    {
        return userAns == questionData.ansMultipleChoice;
    }

}
