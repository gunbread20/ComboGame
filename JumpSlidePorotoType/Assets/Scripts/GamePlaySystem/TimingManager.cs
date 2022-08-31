using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{

    public GameObject player;




    public void TimingCheck(Vector3 pos)
    {

        if (player.transform.position.y < 3.45f && player.transform.position.y > 3.25f)
        {
            Debug.Log("PERFECT!!");
        }
        else if (player.transform.position.y < 3.25f && player.transform.position.y > 3.05f)
        {
            Debug.Log("GOOD!!");
        }
        else if (player.transform.position.y < 3.05f && player.transform.position.y > 2.85f)
        {
            Debug.Log("NORMAL!!");
        }
        else
        {
            Debug.Log("ºé½¼");
        }
    }
}
