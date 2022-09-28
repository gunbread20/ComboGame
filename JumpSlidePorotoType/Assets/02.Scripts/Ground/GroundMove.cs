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
    FeverManager feverManager;

    public Transform[] allChild;
    public Rigidbody[] allRigid;
    public List<Vector3> objOriginPos = new List<Vector3>();
    public List<Vector3> objOriginRot = new List<Vector3>();
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
        feverManager = FindObjectOfType<FeverManager>();

        
        allChild = GetComponentsInChildren<Transform>();
        allRigid = GetComponentsInChildren<Rigidbody>();

        for (int i = 1; i < allChild.Length; i++)
        {
            objOriginPos.Add(allChild[i].localPosition);
            objOriginRot.Add(allChild[i].localRotation.eulerAngles);
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
            if(feverManager.offFeverTime < 3f)
            {
                time += Time.deltaTime;

                feverSpeed = Mathf.Lerp(2.5f, 1f, time);
                return;
            }
                

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

            for (int i = 1; i < allChild.Length; i++)
            {
                allChild[i].localPosition = objOriginPos[i - 1];
                allChild[i].localEulerAngles = objOriginRot[i - 1];
            }

            for (int i = 0; i < allRigid.Length; i++)
            {
                if (allRigid.Length != 0)
                {
                    allRigid[i].velocity = Vector3.zero;
                    allRigid[i].angularVelocity = Vector3.zero;
                }
            }


                gameObject.SetActive(false);

            
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
