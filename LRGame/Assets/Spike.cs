using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    GameObject cameraObj;
    Rigidbody rb;
    SwipeSet SwipeSet;
    CatchBall CatchBall;

    Vector2 screenSize;
    Vector2 startPos;
    Vector2 swipeInput;
    Vector3 forceInput;

    float xOffset;
    float zOffset;
    float spikeMagnitude;
    float xComponent;
    float yComponent;
    float zComponent;
    float xzMagnitude;

    public bool thirdTouchAllowed = true;
    public bool thirdTouch = false;
    public float playerHitHight = 3.5f;

    RaycastHit hit;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        screenSize = new Vector2(Screen.width, Screen.height);
        SwipeSet = GameObject.FindWithTag("Ball").GetComponent<SwipeSet>();
        CatchBall = GameObject.FindWithTag("Ball").GetComponent<CatchBall>();
        cameraObj = GameObject.FindWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
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
                        if (hit.transform.tag == "Ball" && thirdTouchAllowed && transform.position.y < playerHitHight && SwipeSet.secondTouchAllowed == false)
                        {
                            // Record initial touch position.
                            startPos = touch.position;
                            thirdTouch = true;
                        }
                    }
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    break;

                case TouchPhase.Ended:

                    if (thirdTouch && thirdTouchAllowed)
                    {
                        // Determine direction by comparing the end touch position with the initial one
                        swipeInput = touch.position - startPos;

                        // spikeMagnitude [0,1]
                        spikeMagnitude = Mathf.Sqrt(2 * (swipeInput.x / screenSize.x) * (swipeInput.x / screenSize.x) + (swipeInput.y / screenSize.y) * (swipeInput.y / screenSize.y));
                        xOffset = cameraObj.transform.position.x - transform.position.x;
                        zOffset = cameraObj.transform.position.z - transform.position.z;
                        xzMagnitude = Mathf.Sqrt(xOffset * xOffset + zOffset * zOffset);
                        Debug.Log("xOffset = " + xOffset);
                        Debug.Log("zOffset = " + zOffset);
                        Debug.Log("xzMagnitude = " + xzMagnitude);
                        // xComponent = swipeInput.x / screenSize.x - xOffset;
                        // xComponent = (1 - Mathf.Abs(transform.position.x / 9)) * swipeInput.x / screenSize.x;
                        // xComponent = - (xOffset - 4 * swipeInput.x / screenSize.x) * spikeMagnitude;
                        xComponent = - xOffset / xzMagnitude + 2 * swipeInput.x / screenSize.x * spikeMagnitude;
                        yComponent = 2 * (swipeInput.y / screenSize.y + 0.5f);
                        // zComponent = 1.5f - Mathf.Abs(swipeInput.y / screenSize.y);
                        zComponent = Mathf.Abs(zOffset) / 2 / xzMagnitude - (1 -  Mathf.Abs(transform.position.x / 12)) * Mathf.Abs(swipeInput.x / screenSize.x) + 2 * Mathf.Abs(swipeInput.y / screenSize.y);

                        Debug.Log("xComponent = " + xComponent);
                        Debug.Log("yComponent = " + yComponent);
                        Debug.Log("zComponent = " + zComponent);

                        forceInput = new Vector3(xComponent, yComponent, zComponent) * 500;
                        // apply force
                        rb.AddForce(forceInput);
                        // end slow motion
                        Time.timeScale = 1;
                        Time.fixedDeltaTime = 0.02F * Time.timeScale;

                        // configure state of values
                        thirdTouchAllowed = false;
                        CatchBall.enabled = false;
                    }
                    break;
            }
        }
    }
}
