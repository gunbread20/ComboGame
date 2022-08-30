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

    // Update is called once per frame
    void Update()
    {
        if (GroundManager.instance.state == GameState.PAUSE)
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
            transform.DOMoveX(-2f, 0.5f).SetLoops(2, LoopType.Yoyo);
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.DOMoveX(2f, 0.5f).SetLoops(2, LoopType.Yoyo);
        }

        /*
        if (Input.GetKey(KeyCode.Space))
        {
            timer += Time.deltaTime;

            if (timer >= 0.3)
            {
                transform.localScale = new Vector3(oriScale.x, (oriScale.y / 2), oriScale.z);

                if(!isJump && transform.position.y == 1)
                transform.position = new Vector3(oriPos.x, 0.5f, oriPos.z);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (timer < 0.3 && !isJump)
            {
                rg.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isJump = true;
            }

            timer = 0;
            
            if (transform.position.y < 1.9)
            {
                transform.position = new Vector3(oriPos.x, 1f, oriPos.z);
            }

            if (transform.localScale.y == 1)
            {
                transform.localScale = oriScale;
            }
        }
        */
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GroundManager.instance.GameOver();
            GroundManager.instance.state = GameState.PAUSE;
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
