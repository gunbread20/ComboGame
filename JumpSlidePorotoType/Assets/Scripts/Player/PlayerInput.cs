using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SwipeDir
{ 
    UP,
    DOWN,
    RIGHT,
    LEFT,
    TOUCH,
    NONE
}

public class PlayerInput : MonoBehaviour
{
    private Vector2 touchBeganPos;
    private Vector2 touchEndedPos;
    private Vector2 touchDif;
    private float swipeSensitivity;
    public PlayerControl playerCon;

    public SwipeDir swipe;


    private void Awake()
    {
        playerCon = GetComponent<PlayerControl>();
    }

    // Start is called before the first frame update
    void Start()
    {
        swipe = SwipeDir.NONE;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchBeganPos = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                touchEndedPos = touch.position;
                touchDif = (touchEndedPos - touchBeganPos);

                if (Mathf.Abs(touchDif.y) > swipeSensitivity || Mathf.Abs(touchDif.x) > swipeSensitivity)
                {
                    if (touchDif.y > 0 && Mathf.Abs(touchDif.y) > Mathf.Abs(touchDif.x))
                    {
                        swipe = SwipeDir.UP;
                    }
                    else if (touchDif.y < 0 && Mathf.Abs(touchDif.y) > Mathf.Abs(touchDif.x))
                    {
                        swipe = SwipeDir.DOWN;
                    }
                    else if (touchDif.x > 0 && Mathf.Abs(touchDif.x) > Mathf.Abs(touchDif.y))
                    {
                        swipe = SwipeDir.RIGHT;
                    }
                    else if (touchDif.x < 0 && Mathf.Abs(touchDif.x) > Mathf.Abs(touchDif.y))
                    {
                        swipe = SwipeDir.LEFT;
                    }
                }

                else
                {
                    swipe = SwipeDir.TOUCH;
                }

                playerCon.PlayerMovement(swipe);
            }
        }
    }
}
