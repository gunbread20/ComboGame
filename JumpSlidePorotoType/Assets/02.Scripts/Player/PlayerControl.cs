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
    public float jumpHeight = 2.5f;

    public float slideSize;
    public float sideLength;
    public float speed;

    public float invincibleTime;
    private float invincibleMinTime;
    private float invincibleMaxTime;

    public bool isFever = false;

    public TouchStatus touchState;

    ScoreManager scoreManager;
    PlayerHealth playerHealth;

    void Start()
    {
        invincibleMinTime = invincibleTime;
        invincibleMaxTime = invincibleTime * GroundManager.Instance.maxTimeSpeed;

        GroundManager.Instance.speedUp.AddListener(AddInvincibleTime);
        GroundManager.Instance.speedClear.AddListener(ClearInvincibleTime);

        rg = GetComponentInChildren<Rigidbody>();
        scoreManager = FindObjectOfType<ScoreManager>();
        playerHealth = GetComponent<PlayerHealth>();
        oriScale = transform.localScale;
        isJump = false;
        isInvincible = false;
        startJump = false;
        touchState = TouchStatus.NONE;
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
            //other.gameObject.SetActive(false);
            other.GetComponent<GemAnimationScript>().SetOff(); ;
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

        if (transform.position.y > jumpHeight)
        {
            rg.velocity = Vector3.zero;
            rg.velocity = (Vector3.down * (jumpForce / 2));
        }

        if (Input.GetMouseButton(0))
        {
            touchState = TouchStatus.DRAG;
            float ClickPosX = Input.mousePosition.x - widthHalf;
            float widthSixer = widthHalf / 6;

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
            touchState = TouchStatus.NONE;
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
            float sensivity = 0.1f;

            if (Mathf.Abs(playerX - transform.position.x) > sensivity)
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
            else
            {
                transform.position = new Vector3(playerX, transform.position.y, transform.position.z);
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

    void AddInvincibleTime()
    {
        invincibleTime = invincibleTime * (Time.timeScale + GroundManager.Instance.addTimeSpeed);

        if (invincibleTime > invincibleMaxTime)
        {
            invincibleTime = invincibleMaxTime;
        }
    }

    void ClearInvincibleTime()
    {
        invincibleTime = invincibleMinTime;
    }
}
