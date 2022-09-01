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
    private GameObject[] gems;

    ScoreManager scoreManager;
    TimingManager timingManager;
    // Start is called before the first frame update

    private void Awake()
    {
        for (int i = 0; i < gems.Length; i++)
        {
            gems[i].SetActive(false);
        }
    }

    void Start()
    {
        moveSpeed = GroundManager.Instance.speed;
        scoreManager = FindObjectOfType<ScoreManager>();
        timingManager = FindObjectOfType<TimingManager>();
        GemSpawn();
        jumped = false;
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
        if (gems.Length < 1)
        {
            return;
        }

        float rNum;
        int rPos;

        rNum = Random.Range(0f, 100f);

        if (rNum <= GroundManager.Instance.gemChance * 100)
        {
            rPos = Random.Range(0, gems.Length);
            gems[rPos].SetActive(true);
        }
    }
}
