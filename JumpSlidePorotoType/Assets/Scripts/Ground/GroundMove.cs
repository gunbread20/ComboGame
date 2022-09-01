using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GroundMove : MonoBehaviour
{
    public GroundManager groundManager;

    public int groundNum;
    public float moveSpeed;
    private bool jumped;

    [SerializeField]
    private GameObject gem;

    ScoreManager scoreManager;
    TimingManager timingManager;
    // Start is called before the first frame update

    private void Awake()
    {

    }

    private void OnEnable()
    {
        gem.SetActive(false);
        jumped = false;
        GemSpawn();
    }

    void Start()
    {
        moveSpeed = GroundManager.Instance.speed;
        scoreManager = FindObjectOfType<ScoreManager>();
        timingManager = FindObjectOfType<TimingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.state == GameState.PAUSE)
        {
            return;
        }

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
            Destroy(this.gameObject);
        }
    }

    private void GemSpawn()
    {
        if (gem == null)
        {
            return;
        }

        float rNum;
        int rPos;

        rNum = Random.Range(0f, 100f);

        if (rNum <= GroundManager.Instance.gemChance * 100)
        {
            gem.SetActive(true);
        }
    }
}
