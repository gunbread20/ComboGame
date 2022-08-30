using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rg;

    float timer;
    [SerializeField]
    bool isJump;
    Vector3 oriScale;
    Vector3 oriPos;

    public float jumpForce;

    ScoreManager scoreManager;
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        scoreManager = FindObjectOfType<ScoreManager>();
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
                if (transform.localScale.y <= 1)
                {
                    transform.DOScaleY(2f, 0.1f);
                }

                rg.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isJump = true;
            }
        }

        else if (dir == SwipeDir.DOWN)
        {
            transform.DOScaleY(1f, 0.1f);

            rg.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);
        }

        else if (dir == SwipeDir.LEFT)
        {
            if (Mathf.Abs(transform.position.x) >= 0.3)
                return;

            transform.DOMoveX(0, 0.1f);
            transform.DOMoveX(-2f, 0.5f).SetLoops(2, LoopType.Yoyo);
        }

        else if (dir == SwipeDir.RIGHT)
        {
            if (Mathf.Abs(transform.position.x) >= 0.3)
                return;

            transform.DOMoveX(0, 0.1f);
            transform.DOMoveX(2f, 0.5f).SetLoops(2, LoopType.Yoyo);
        }
    }

    void Update()
    {
        if (GameManager.instance.state == GameState.PAUSE)
        {
            return;
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
            scoreManager.GameOverScore();
            GameManager.instance.GameOver();

            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isJump = false;
        }
    }
}
