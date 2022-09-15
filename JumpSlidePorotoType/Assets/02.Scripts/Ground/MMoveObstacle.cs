using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MMoveObstacle : MonoBehaviour
{
    private void Start()
    {
        transform.tag = "Ground";
    }

    private void OnEnable()
    {
        transform.tag = "Ground";

    }

    private void Update()
    {
        if (GameObject.Find("Player").GetComponent<PlayerControl>().touchState == TouchStatus.NONE)
        {
            transform.tag = "Obstacle";
        }
        else
        {
            transform.tag = "Ground";
        }
    }
}
