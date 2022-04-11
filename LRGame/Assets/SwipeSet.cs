using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeSet : MonoBehaviour
{
    CatchBall CatchBall;
    Rigidbody rb;

    Vector2 screenSize;
    Vector2 startPos;
    Vector2 swipeInput;
    Vector2 forceInput;

    public bool secondTouchAllowed = true;
    public bool secondTouch = false;

    RaycastHit hit;

    float slowMoFct;
    float slowMoStartY = 6.5f;
    float setPositionY = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        screenSize = new Vector2(Screen.width, Screen.height);
        CatchBall = GameObject.FindWithTag("Ball").GetComponent<CatchBall>();
        CatchBall.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //slow falling ball before set
        if (secondTouchAllowed && rb.position.y < slowMoStartY && Time.timeScale > 0.01f)
        {
            // slowMoFct = 1f - Mathf.Sqrt((rb.position.y - setPositionY)/(slowMoStartY - setPositionY));
            slowMoFct = 1f - 1 / (2 * (rb.position.y - setPositionY));
            if (slowMoFct < 0.01f)
            {
                slowMoFct = 0.01f;
            }
            Time.timeScale = slowMoFct;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }

        // Track a single touch as a direction control.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:

                    // The ray to the touched object in the world
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray.origin, ray.direction, out hit))
                    {
                        if (hit.transform.tag == "Ball" && secondTouchAllowed)
                        {
                            // Record initial touch position.
                            startPos = touch.position;
                            secondTouch = true;
                        }
                    }
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    break;

                case TouchPhase.Ended:

                    if (secondTouch && secondTouchAllowed)
                    {
                        // Determine direction by comparing the end touch position with the initial one
                        swipeInput = touch.position - startPos;
                        forceInput = new Vector2(swipeInput.x / screenSize.x, swipeInput.y / screenSize.y) * 1500;
                        // apply force
                        rb.AddForce(forceInput);
                        // end slow motion
                        Time.timeScale = 1;
                        Time.fixedDeltaTime = 0.02F * Time.timeScale;

                        // configure state of values
                        secondTouchAllowed = false;
                        CatchBall.enabled = true;
                    }
                    break;
            }
        }
    }
}
