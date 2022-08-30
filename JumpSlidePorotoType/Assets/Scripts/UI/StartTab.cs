using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTab : MonoBehaviour
{
    public GameObject tabUI;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) || Input.touchCount > 0)
        {
            GameManager.instance.state = GameState.RUNNING;
            Destroy(tabUI);
            Destroy(this.gameObject);
        }       
    }
}
