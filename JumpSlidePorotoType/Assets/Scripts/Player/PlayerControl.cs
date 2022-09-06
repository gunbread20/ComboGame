using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody rg;

    float timer;
    [SerializeField]
    bool isJump;
    bool isInvincible;

    Vector3 oriScale;
    Vector3 oriPos;

    public float jumpForce;
    public float slideSize;
    public float sideLength;
    public float speed;
    public float invincibleTime;

    public bool isFever = false;

    ScoreManager scoreManager;
    PlayerHealth playerHealth;

    void Start()
    {
        rg = GetComponentInChildren<Rigidbody>();
        scoreManager = FindObjectOfType<ScoreManager>();
        playerHealth = GetComponent<PlayerHealth>();
        oriScale = transform.localScale;
        isJump = false;
        isInvincible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            if(isFever)
            {
                playerHealth.FeverTouch(other.gameObject, other.ClosestPoint(transform.position));
                return;
            }
            playerHealth.Damaged();
        }

        if (other.CompareTag("Gem"))
        {
            other.gameObject.SetActive(false);
            GameManager.instance.GetGem();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isJump = false;
        }
    }

    void Update()
    {
        float widthHalf = Screen.width / 2;

        if (GameManager.instance.state == GameState.PAUSE)
        {
            return;
        }

        if (transform.position.y > 2.7f)
        {
            rg.velocity = Vector3.zero;
        }

        if (Input.GetMouseButton(0))
        {
            float ClickPosX = Input.mousePosition.x - widthHalf;
            float widthSixer = widthHalf / 12;

            if (Mathf.Abs(ClickPosX) <= Mathf.Abs(widthHalf - widthSixer))
            {
                ClickPosX = Mathf.Clamp(ClickPosX, -(widthHalf - widthSixer), (widthHalf - widthSixer));
                float playerPosX = sideLength * (ClickPosX / (widthHalf - widthSixer));
                PlayerMovement(TouchStatus.DRAG, playerPosX);
            }
            else
            {
                float maxPosX = sideLength * Mathf.Sign(ClickPosX);
                PlayerMovement(TouchStatus.DRAG, maxPosX);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            PlayerMovement(TouchStatus.NONE, 0);
        }
    }

    public void PlayerMovement(TouchStatus dir, float playerX)
    {
        if (GameManager.instance.state == GameState.PAUSE)
        {
            return;
        }

        if (dir == TouchStatus.DRAG)
        {
            float way = Mathf.Sign(playerX - transform.position.x);
            float xSpeed = way * speed * Time.deltaTime;
            if (Mathf.Abs(playerX - transform.position.x) > 0.05)
            {
                if (Mathf.Abs(transform.position.x + xSpeed) <= sideLength)
                {
                    transform.position += new Vector3(xSpeed, 0, 0);
                }
                else
                {
                    transform.position = new Vector3(sideLength * way, transform.position.y, transform.position.z);
                }

            }
        }

        if (dir == TouchStatus.NONE)
        {
            if (!isJump)
            {
                rg.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isJump = true;
            }
        }
    }
}
