using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GroundMove : MonoBehaviour
{
    public int groundNum;
    public float moveSpeed;
    private bool jumped;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = GroundManager.instance.speed;
        jumped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GroundManager.instance.state == GameState.PAUSE)
        {
            return;
        }

        transform.position += new Vector3(0, 0, -(Time.deltaTime) * moveSpeed);

        if (transform.position.z <= 0 && jumped == false && groundNum != 0)
        {
            GroundManager.instance.AddScore();
            jumped = true;
        }

        if (transform.position.z <= -10)
        {
            GroundManager.instance.SpawnGround();
            Destroy(this.gameObject);
        }
    }
}
