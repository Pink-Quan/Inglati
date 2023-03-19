using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    [Header("Loading UI")]
    public GameObject loadingUIParent;
    public Image loadingBG;
    public Image loadingBar;
    public Text loadingText;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadToScene(string name)
    {
        StartCoroutine(StartChangeScene(name));
    }    

    private IEnumerator StartChangeScene(string name)
    {
        loadingBG.color = new Color(loadingBG.color.r, loadingBG.color.g, loadingBG.color.b, 0);
        loadingBar.color = new Color(loadingBar.color.r, loadingBar.color.g, loadingBar.color.b, 1);
        loadingText.color = new Color(loadingBar.color.r, loadingBar.color.g, loadingBar.color.b, 1);
        loadingBar.fillAmount = 0;

        loadingUIParent.SetActive(true);
        loadingBG.DOFade(1, 0.5f);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(name);
        float clk = 0;
        while(clk<=2)
        {
            yield return null;
            clk += Time.deltaTime;
            loadingBar.fillAmount = clk / 2f;
        }
        loadingBar.fillAmount = 1;

        loadingBG.DOFade(0, 0.5f);
        loadingText.DOFade(0, 0.5f);
        loadingBar.DOFade(0, 0.5f);

        yield return new WaitForSeconds(0.5f);
        loadingUIParent.SetActive(false);
    }
}
