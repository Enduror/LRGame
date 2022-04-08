using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeSet : MonoBehaviour
{
    Rigidbody rb;
    Vector2 startPos;
    Vector2 swipeInput;
    private Vector2 forceInput;
    public bool inputAllowed = true;

    Vector2 screenSize;
    //raycast handling
    RaycastHit hit;
    public bool hitBall = false;
    int TapCount;
    float slowMoStartY = 6.5f;
    float setPositionY = 2.5f;
    CatchBall CatchBall;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        TapCount = 0;
        screenSize = new Vector2(Screen.width, Screen.height);
        CatchBall = GetComponent<CatchBall>();
        CatchBall.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //slow falling ball before set
        if (inputAllowed && rb.position.y < slowMoStartY && Time.timeScale > 0.01f)
            // Time.timeScale = 1f - Mathf.Sqrt((rb.position.y - setPositionY)/(slowMoStartY - setPositionY));
            Time.timeScale = 1f - 1 / (2 * (rb.position.y - setPositionY));
            Time.fixedDeltaTime = 0.02F * Time.timeScale;


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
                        if (hit.transform.tag == "Ball" && inputAllowed)
                        {
                            // Record initial touch position.
                            startPos = touch.position;
                            hitBall = true;
                        }
                    }
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    break;

                case TouchPhase.Ended:

                    if (hitBall && inputAllowed)
                    {
                        // Determine direction by comparing the end touch position with the initial one
                        swipeInput = touch.position - startPos;
                        forceInput = new Vector2(swipeInput.x / screenSize.x, swipeInput.y / screenSize.y) * 500;
                        // Report that the touch has ended when it ends
                        Time.timeScale = 1;
                        Time.fixedDeltaTime = 0.02F * Time.timeScale;
                        //ApplyForce();
                        rb.AddForce(forceInput);
                        inputAllowed = false;
                        CatchBall.enabled = true;
                        GameObject.Find("Main Camera").GetComponent<CameraControl>().enabled = true;
                    }
                    else
                    {
                        TapCount++;
                    }
                    if (TapCount > 1)
                    {
                        ResetBall();
                        TapCount = 0;
                    }
                    break;
            }
        }
    }
    void ApplyForce()
    {
        rb.AddForce(forceInput);
    }

    void ResetBall()
    {
        rb.position = new Vector3(1f, 7f, -0.5f);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        hitBall = false;
        inputAllowed = true;
        CatchBall.enabled = false;
        CatchBall.spikable = false;
        GameObject.Find("Main Camera").GetComponent<Transform>().position = new Vector3(1f, 4f, -4f);
        GameObject.Find("Main Camera").GetComponent<Transform>().rotation = Quaternion.identity;
        GameObject.Find("Main Camera").GetComponent<CameraControl>().enabled = false;
    }
}
