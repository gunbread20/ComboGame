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
    bool startJump;
    public bool isInvincible;

    Vector3 oriScale;
    Vector3 oriPos;

    public float jumpForce = 7;
    public float jumpHight = 2.4f;
    public float minJumpForce = 7;
    public float maxJumpForce = 9;
    public float addJumpForce = 0.025f;

    public float slideSize;
    public float sideLength;
    public float speed;
    public float invincibleTime;

    public bool isFever = false;

    ScoreManager scoreManager;
    PlayerHealth playerHealth;

    void Start()
    {
        GroundManager.Instance.speedUp.AddListener(PlayerJumpUp);
        GroundManager.Instance.speedClear.AddListener(PlayerJumpClear);

        rg = GetComponentInChildren<Rigidbody>();
        scoreManager = FindObjectOfType<ScoreManager>();
        playerHealth = GetComponent<PlayerHealth>();
        oriScale = transform.localScale;
        isJump = false;
        isInvincible = false;
        startJump = false;
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
            else if (isInvincible)
            {
                Debug.Log("무적상태");
            }
            else
            {
                playerHealth.Damaged();
                InvincibleEffect();
                isInvincible = true;
                Invoke("InvincibleOff", invincibleTime);
            }  
        }



        if (other.CompareTag("Gem"))
        {
            other.gameObject.SetActive(false);
            GameManager.instance.GetGem();
        }
    }

    public void InvincibleEffect()
    {
        Debug.Log(GetComponentInChildren<MeshRenderer>().materials[0]);
        GetComponentInChildren<MeshRenderer>().materials[0].DOFade(0, 0.3f).SetLoops(6, LoopType.Yoyo);
    }

    public void InvincibleOff()
    {
        isInvincible = false;
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
        else if (startJump == false)
        {
            Invoke("StartJumpCancle", 0.5f);
            return;
        }

        if (transform.position.y > jumpHight)
        {
            rg.velocity = Vector3.zero;
            rg.velocity = (Vector3.down * (jumpForce / 2));
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

    private void StartJumpCancle()
    {
        startJump = true;
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

    private void PlayerJumpUp()
    {
        jumpForce += addJumpForce;
        if (jumpForce > maxJumpForce)
            jumpForce = maxJumpForce;
    }

    private void PlayerJumpClear()
    {
        jumpForce = minJumpForce;
    }
}
