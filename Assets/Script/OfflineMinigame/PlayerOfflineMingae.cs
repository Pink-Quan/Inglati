using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOfflineMingae : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float moveRange=4.46f;
    [SerializeField] private Vector3 playerBasePos = new Vector3(0, -4.3f);

    [Header("Controll")]
    [SerializeField] private KeyCode leftKey=KeyCode.A;
    [SerializeField] private KeyCode rightKey=KeyCode.D;
    [SerializeField] private KeyCode fireKey=KeyCode.J;
    [SerializeField] private KeyCode removeKey=KeyCode.K;

    [Header("Stats")]
    [SerializeField] private float speed;

    [Header("UI")]
    [SerializeField] private Text timeText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text lifeText;
    [SerializeField] private GameObject bulletUIPrefab;
    [SerializeField] private Transform bulletsContainer;

    [Header("Attack")]
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float laserAliveTime = 0.1f;
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private ParticleSystem missEffect;

    [Header("Sounds")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip damgeSound;
    [SerializeField] private AudioClip removeBulletSound;

    private float moveVec=0f;

    private float time = 0;
    private int life = 10;
    private int score = 0;

    private Queue<EnglishBall> bullets=new Queue<EnglishBall>();
    private EnglishBall bullet;

    private void Start()
    {
        for(int i=0;i<3;i++)
        {
            var t = OfflineMinigameManger.instance.GetRandomBallInfor();
            SpawnBullet(t);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerBasePos - new Vector3(moveRange, 0), playerBasePos + new Vector3(moveRange, 0));
    }

    private void FixedUpdate()
    {
        moveVec = 0f;

        if (Input.GetKey(rightKey)) moveVec = 1;
        if (Input.GetKey(leftKey)) moveVec = -1;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x+speed*Time.deltaTime*moveVec,
            playerBasePos.x-moveRange,
            playerBasePos.x+moveRange), 

            transform.position.y);
    }
    private void Update()
    {
        if (Input.GetKeyDown(fireKey))
            Fire();
        if (Input.GetKeyDown(removeKey))
            RemoveBullet();

        time += Time.deltaTime;
        timeText.text = (Mathf.Round(time*100)/100).ToString();
    }

    private void SpawnBullet(EnglishBall bullet)
    {
        bullets.Enqueue(bullet);
        this.bullet = bullets.Peek();
        var t = Instantiate(bulletUIPrefab, bulletsContainer);
        t.transform.SetAsLastSibling();
        t.transform.GetChild(0).GetComponent<Text>().text = bullet.name;
    }
    private void RemoveBullet()
    {
        bullets.Dequeue();
        Destroy(bulletsContainer.GetChild(0).gameObject);
        bullet = bullets.Peek();
        audioSource.PlayOneShot(removeBulletSound);
        SpawnBullet(OfflineMinigameManger.instance.GetRandomBallInfor());
    }
    private void Fire()
    {
        var hit = Physics2D.Raycast(transform.position, Vector2.up,10);
        if(!hit)
        {
            var t = Instantiate(missEffect);
            t.transform.position = transform.position;
            LoseLife(1);
            RemoveBullet();
            return;
        }
        hit.collider.TryGetComponent(out IconEntity ballHitted);

        OnLaser(hit.point);
        Invoke("OffLaser", laserAliveTime);

        if (ballHitted != null)
        {
            if(ballHitted.infor.name == bullet.name)
            {
                var t = Instantiate(hitEffect);
                t.transform.position = hit.point;
                Score();
                OfflineMinigameManger.instance.ReMoveBall(ballHitted);
                Destroy(ballHitted.gameObject);
            }
            else
            {
                var t = Instantiate(missEffect);
                t.transform.position = transform.position;
                LoseLife(1);
            }
        }
        else
        {
            var t = Instantiate(missEffect);
            t.transform.position = transform.position;
            LoseLife(1);
        }

        RemoveBullet();
    }

    private void Score()
    {
        audioSource.PlayOneShot(explosionSound);
        score++;
        scoreText.text = score.ToString();
    }

    private void OnLaser(Vector3 aimPos)
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, aimPos);
    }
    private void OffLaser()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position);
    }

    public void LoseLife(int damge)
    {
        life-=damge;
        lifeText.text = life.ToString();
        audioSource.PlayOneShot(damgeSound);
        if(life<=0)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            this.enabled = false;
            OfflineMinigameManger.instance.GameOver(score,time);
        }    
    }
    public void Healing(int heal)
    {
        life += heal;
        lifeText.text = life.ToString();
    }
}

