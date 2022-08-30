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

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
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
                rg.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                if (transform.localScale.y <= 1)
                {
                    transform.DOScaleY(2f, 0.1f);
                }

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
            if (transform.position.x != 0)
                return;

            transform.DOMoveX(-2f, 0.5f).SetLoops(2, LoopType.Yoyo);
        }
        else if (dir == SwipeDir.RIGHT)
        {
            if (transform.position.x != 0)
                return;
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
        {
            if (!isJump)
            {
                rg.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

                if (transform.localScale.y <= 1)
                {
                    transform.DOScaleY(2f, 0.1f);
                }

                isJump = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.DOScaleY(1f, 0.1f);

            rg.AddForce(Vector3.down * jumpForce, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (transform.position.x != 0)
                return;
            
            transform.DOMoveX(-2f, 0.5f).SetLoops(2, LoopType.Yoyo);
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (transform.position.x != 0)
                return;
            transform.DOMoveX(2f, 0.5f).SetLoops(2, LoopType.Yoyo);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.instance.GameOver();
            GameManager.instance.state = GameState.PAUSE;
            Destroy(this.gameObject);
        }

        if (other.CompareTag("Power"))
        {
            Destroy(other.gameObject);
            isJump = false;
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
