using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    PlayerControl playerControl;

    [SerializeField]
    private float zPos;

    private void OnEnable()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        GemSetting();

        if (playerControl == null)
        {
            Destroy(this.gameObject);
        }
    }

    private void GemSetting()
    {
        float xPos = playerControl.sideLength;
        int r = Random.Range(0, 3);

        if (r == 1)
            xPos *= -1f;
        else if (r == 2)
            xPos = 0;

        transform.localPosition = new Vector3(xPos, 1, zPos);
    }
}
