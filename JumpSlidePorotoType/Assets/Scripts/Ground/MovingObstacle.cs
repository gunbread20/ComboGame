using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField]
    private float halfWidth = 5;
    [SerializeField]
    private float maxDis = 3;

    private PlayerControl player;

    int way = 1;

    private void Start()
    {
        maxDis = halfWidth - (transform.localScale.x / 2);
        player = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    private void Update()
    {
        if (GameManager.instance.state == GameState.PAUSE)
        {
            return;
        }

        

        if (transform.localPosition.x >= maxDis)
        {
            way = -1;
        }
        else if (transform.localPosition.x <= maxDis * -1)
        {
            way = 1;
        }


        transform.localPosition += new Vector3(way, 0, 0) * Time.deltaTime * (player.speed / 2);
    }
}
