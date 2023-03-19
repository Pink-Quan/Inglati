using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadingGameManager : MultiChoiseExamManager
{
    [SerializeField] private string passage;
    [SerializeField] private GameObject readingPassage;

    [SerializeField] private Text passageUI;
}
