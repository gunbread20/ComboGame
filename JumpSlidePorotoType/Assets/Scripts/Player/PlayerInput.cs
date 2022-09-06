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
    private Vector2 touchBeganPos;
    private Vector2 touchEndedPos;
    private Vector2 touchDif;
    public float swipeSensitivity;
    public PlayerControl playerCon;

    public TouchStatus touchStatus;

    private float widthHalf;
    private float sideLength;

    private float tTime;

    private void Awake()
    {
        playerCon = GetComponent<PlayerControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        touchStatus = TouchStatus.NONE;
        sideLength = playerCon.sideLength;
    }

    // Update is called once per frame
    void Update()
    {
        widthHalf = Screen.width / 2;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            tTime += touch.deltaTime;
            float touchPosX = touch.position.x - widthHalf;
            float widthSixer = widthHalf / 12;

            if (Mathf.Abs(touchPosX) <= Mathf.Abs(widthHalf - widthSixer))
            {
                touchPosX = Mathf.Clamp(touchPosX, -(widthHalf - widthSixer), (widthHalf - widthSixer));
                float playerPosX = sideLength * (touchPosX / (widthHalf - widthSixer));
                playerCon.PlayerMovement(this.touchStatus, playerPosX);
            }

            
        }

        else if (tTime != 0)
        {
            touchStatus = TouchStatus.NONE;
            playerCon.PlayerMovement(touchStatus, 0);
            tTime = 0;
        }
    }
}
