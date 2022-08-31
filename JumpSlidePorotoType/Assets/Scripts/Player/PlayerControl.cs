using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rg;

    float timer;
    [SerializeField]
    bool isJump;
    Vector3 oriScale;
    Vector3 oriPos;

    public float jumpForce;
    public float slideSize;
    public float sideLength;
    public float changeTime;

    ScoreManager scoreManager;
    PlayerHealth playerHealth;

    void Start()
    {
        rg = GetComponent<Rigidbody>();
        scoreManager = FindObjectOfType<ScoreManager>();
        playerHealth = GetComponent<PlayerHealth>();
        oriScale = transform.localScale;
        isJump = false;
    }

    public void PlayerMovement(SwipeDir dir)
    {
        if (GameManager.instance.state == GameState.PAUSE)
        {
            return;
        }

        if (dir == SwipeDir.UP)
        {
            if (!isJump)
            {
               StartCoroutine("SizeUp");
            }
        }

        else if (dir == SwipeDir.DOWN)
        {
            if (transform.localScale.y == oriScale.y)
                StartCoroutine("SizeDown");
        }

        else if (dir == SwipeDir.LEFT)
        {
            if (transform.position.x == sideLength || transform.position.x == oriPos.x)
                StartCoroutine("MoveLeft");
            else
                return;
        }

        else if (dir == SwipeDir.RIGHT)
        {
            if (transform.position.x == -sideLength || transform.position.x == oriPos.x)
                StartCoroutine("MoveRight");
            else
                return;
        }
    }

    void Update()
    {
        if (GameManager.instance.state == GameState.PAUSE)
        {
            return;
        }

        if (transform.position.y > 3f)
        {
            rg.velocity = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            PlayerMovement(SwipeDir.UP);

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            PlayerMovement(SwipeDir.DOWN);

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            PlayerMovement(SwipeDir.LEFT);

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            PlayerMovement(SwipeDir.RIGHT);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            playerHealth.Damaged();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isJump = false;
        }
    }
    IEnumerator SizeUp()
    {
        float t = 0;

        if (transform.localScale.y == slideSize)
        {
            while (transform.localScale.y < oriScale.y)
            {
                t = Mathf.Clamp(t, 0, changeTime);
                transform.localScale = new Vector3(oriScale.x, slideSize + ((oriScale.y - slideSize) * (t / changeTime)), oriScale.z);
                t += Time.deltaTime;
                yield return null;
            }
        }

        rg.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isJump = true;
    }

    IEnumerator SizeDown()
    {
        float t = 0;

        while (transform.localScale.y > slideSize)
        {
            t = Mathf.Clamp(t, 0, changeTime);
            transform.localScale = new Vector3(oriScale.x, oriScale.y - ((oriScale.y - slideSize) * (t / changeTime)), oriScale.z);
            t += Time.deltaTime;
            yield return null;
        }

        rg.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);
    }

    IEnumerator MoveLeft()
    {
        float t = 0;
        float curX = transform.position.x;

        while (transform.position.x > curX - sideLength)
        {
            t = Mathf.Clamp(t, 0, changeTime);
            transform.position = new Vector3(curX -(sideLength * (t / changeTime)), transform.position.y, transform.position.z);
            t += Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector3(curX - sideLength, transform.position.y, transform.position.z);
    }

    IEnumerator MoveRight()
    {
        float t = 0;
        float curX = transform.position.x;

        while (transform.position.x < curX + sideLength)
        {
            t = Mathf.Clamp(t, 0, changeTime);
            transform.position = new Vector3(curX + (sideLength * (t / changeTime)), transform.position.y, transform.position.z);
            t += Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector3(curX + sideLength, transform.position.y, transform.position.z);
    }
}
