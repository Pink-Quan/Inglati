using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void ChangeScence(string name)
    {
        SceneController.instance.LoadToScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
