using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconEntity : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public EnglishBall infor;

    public void Init(EnglishBall infor)
    {
        this.infor = infor;
        spriteRenderer.sprite = infor.image;

        InvokeRepeating("RandomMove", 0.5f, 1.5f);
    }

    private void RandomMove()
    {
        Vector3 randomPoint=new Vector3(
            Random.Range(
                OfflineMinigameManger.instance.minBouch.x, OfflineMinigameManger.instance.maxBouch.x), 
            Random.Range(
                OfflineMinigameManger.instance.minBouch.y, OfflineMinigameManger.instance.maxBouch.y));
        transform.DOMove(randomPoint, 1f);
    }
}
