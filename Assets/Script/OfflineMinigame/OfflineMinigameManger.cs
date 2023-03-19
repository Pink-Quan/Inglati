using ExtendedAnimation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OfflineMinigameManger : MonoBehaviour
{
    public static OfflineMinigameManger instance;
    
    public List<EnglishBall> balls;

    public Vector3 maxBouch;
    public Vector3 minBouch;
    [SerializeField] private float spawnPerRowDuration = 10f;
    [SerializeField] private IconEntity englishBallPrefab;
    [SerializeField] private Text enemyText;

    private int numberBallSpawnPerRow = 3;

    [SerializeField] private PlayerOfflineMingae player;
    [SerializeField] private GameObject gameoverParent;
    [SerializeField] private UIAnimation gameoverBoard;
    [SerializeField] private Text gameoverTime;
    [SerializeField] private Text gameoverScore;
    private List<IconEntity> allBalls=new List<IconEntity>();


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InvokeRepeating("SpawnBalls", 7, spawnPerRowDuration);
    }

    private void SpawnBalls()
    {
        for(int i=0;i<numberBallSpawnPerRow;i++)
        {
            var t = Instantiate(englishBallPrefab);
            t.transform.position= new Vector3(
            Random.Range(
                minBouch.x, maxBouch.x),
            Random.Range(
                minBouch.y, maxBouch.y));
            t.Init(balls[Random.Range(0, balls.Count)]);
            allBalls.Add(t);
        }
        numberBallSpawnPerRow = Random.Range(3, 6);

        if (allBalls.Count > 30) player.LoseLife(allBalls.Count);
        enemyText.text = allBalls.Count.ToString();
        player.Healing(5);
    }
     
    public EnglishBall GetRandomBallInfor()
    {
        return balls[Random.Range(0, balls.Count)];
    }
    public void ReMoveBall(IconEntity t)
    {
        allBalls.Remove(t);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((maxBouch + minBouch) / 2, maxBouch - minBouch);
    }
    public void GameOver(int score,float time)
    {
        CancelInvoke();
        gameoverParent.SetActive(true);
        gameoverBoard.Show();

        gameoverScore.text = score.ToString();
        gameoverTime.text = (Mathf.Round(time * 100) / 100).ToString();

    }
    public void PlayAgain()
    {
        Time.timeScale = 1;
        SceneController.instance.LoadToScene("OfflineMiniGame");
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
public struct EnglishBall
{
    public string name;
    public Sprite image;
}