using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlineMapManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private ArenaLocationData[] arenaLocationDatas;

    [SerializeField] private bool onDrawGizmos;

    [SerializeField] private GameObject enterRoomButton;
    [SerializeField] private Text enterRoomText;
    private string canEnterRoomName;

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

    private void OnDrawGizmos()
    {
        if (!onDrawGizmos) return;
        for(int i=0;i<arenaLocationDatas.Length;i++)
        {
            if (arenaLocationDatas[i].location == null) continue;
            arenaLocationDatas[i].location.name = arenaLocationDatas[i].name;
            Gizmos.DrawWireSphere(arenaLocationDatas[i].location.position, arenaLocationDatas[i].enterRadius);
        }
    }

    private void Update()
    {
        bool isOnButton = false;
        foreach(var a in arenaLocationDatas)
        {
            if (a.location == null) continue;
            float r2 = Vector2.SqrMagnitude(player.transform.position - a.location.position);
            if (r2 <= Mathf.Pow(a.enterRadius,2))
            {
                isOnButton = true;
                canEnterRoomName = a.sceneName;
                enterRoomText.text = a.name;
            }
        }
        enterRoomButton.SetActive(isOnButton);
    }

    public void EnterRoom()
    {
        if (canEnterRoomName == "NA") return; //Only some room

        SceneController.instance.LoadToScene(canEnterRoomName);
    }
}

[System.Serializable]
public struct ArenaLocationData
{
    public string name;
    public Transform location;
    public float enterRadius;
    public string sceneName;
}
