using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GroundMove : MonoBehaviour
{
    public GroundManager groundManager;

    public int groundNum;
    public float moveSpeed;
    public bool gemActive;
    private bool jumped;

    [SerializeField]
    private GameObject[] gem;

    ScoreManager scoreManager;
    PlayerControl playerControl;

    public List<Vector3> objOriginPos = new List<Vector3>();

    float feverSpeed;
    float slowTime;
    float time;

    private void Awake()
    {
        time = 0;
        feverSpeed = 1;
    }

    private void OnEnable()
    {   
        jumped = false;
        gemActive = GroundManager.Instance.gemChance;
        GemSpawn();
    }

    void Start()
    {
        slowTime = GroundManager.Instance.feverSlowTime;
        moveSpeed = GroundManager.Instance.speed;
        scoreManager = FindObjectOfType<ScoreManager>();
        playerControl = FindObjectOfType<PlayerControl>();

        for (int i = 0; i < transform.childCount; i++)
        {
            objOriginPos.Add(transform.GetChild(i).transform.localPosition);
        }
    }

    void Update()
    {
        if (GameManager.instance.state == GameState.PAUSE)
        {
            return;
        }

        if(playerControl.isFever)
        {
            feverSpeed = 2.5f;
        }
        else
        {
            feverSpeed = 1;
        }

        moveSpeed = GroundManager.Instance.speed * feverSpeed;
        transform.position += new Vector3(0, 0, -(Time.deltaTime) * moveSpeed);

        if (transform.position.z <= 0 && jumped == false && groundNum != 0)
        {
            scoreManager.AddScore();
            //timingManager.TimingCheck(transform.position);
            jumped = true;
        }

        if (transform.position.z <= -10)
        {
            GroundManager.Instance.SpawnGround();
            gameObject.SetActive(false);

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).transform.localPosition = objOriginPos[i];
                if (transform.GetChild(i).GetComponent<Rigidbody>() == null)
                    return;
                transform.GetChild(i).GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }

    private void GemSpawn()
    {
        if (gem == null)
        {
            return;
        }

        if (gemActive == false)
        {
            for (int i = 0; i < gem.Length; i++)
            {
                gem[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < gem.Length; i++)
            {
                gem[i].SetActive(true);
            }
        }

        //float rNum;
        //int rPos;

        //rNum = Random.Range(0f, 100f);

        //if (rNum <= GroundManager.Instance.gemChance * 100)
        //{
            
        //}
    }
}
