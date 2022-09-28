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
    TouchStatus touchState;
    float sideLength;
    PlayerControl playerControl;

    private void Start()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
    }

    private void Update()
    {
#if UNITY_EDITOR
        return;
#endif

        float widthHalf = 0.5f;
        Touch touch = Input.GetTouch(0);

        if (Input.GetMouseButton(0))
        {
            touchState = TouchStatus.DRAG;
            float ClickPosX = Camera.main.ScreenToViewportPoint(touch.position).x - widthHalf;
            float widthLimit = 0.425f;

            if (Mathf.Abs(ClickPosX) <= Mathf.Abs(widthLimit))
            {
                ClickPosX = Mathf.Clamp(ClickPosX, -(widthLimit), (widthLimit));
                float playerPosX = sideLength * (ClickPosX / widthHalf);
                playerControl.PlayerMovement(TouchStatus.DRAG, playerPosX);
            }
            else
            {
                float maxPosX = sideLength * Mathf.Sign(ClickPosX);
                playerControl.PlayerMovement(TouchStatus.DRAG, maxPosX);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            touchState = TouchStatus.NONE;
            playerControl.PlayerMovement(TouchStatus.NONE, 0);
        }
    }

}
