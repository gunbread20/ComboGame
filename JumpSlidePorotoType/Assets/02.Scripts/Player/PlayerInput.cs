using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TouchStatus
{ 
    DRAG,
    NONE
}

public class PlayerInput : MonoBehaviour
{
    private void Update()
    {
        Vector2 viewPort = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        Debug.Log(viewPort);
    }

}
