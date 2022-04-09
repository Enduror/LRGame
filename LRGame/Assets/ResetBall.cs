using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBall : MonoBehaviour
{
    Rigidbody rb;
    SwipeSet SwipeSet;
    CatchBall CatchBall;
    public ResetCamera ResetCamera;
    public int TapCount;
    bool ballTap;
    //raycast handling
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        SwipeSet = GameObject.FindWithTag("Ball").GetComponent<SwipeSet>();
        CatchBall = GameObject.FindWithTag("Ball").GetComponent<CatchBall>();
        //ResetCamera = GameObject.FindWithTag("Main Camera").GetComponent<ResetCamera>();
        rb = GetComponent<Rigidbody>();
        ballTap = false;
        TapCount = 0;
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
                        if (hit.transform.tag == "Ball")
                        {
                            ballTap = true;
                        }
                    }
                    break;

                case TouchPhase.Ended:
                    if (ballTap)
                    {
                        ballTap = false;
                    }
                    else
                    {
                        TapCount++;
                    }
                    if (TapCount > 1)
                    {
                        // list reset methodes here
                        BallToBeforeSet();
                        ResetSlowMo();
                        ResetCamera.RestCameraPosition();
                        TapCount = 0;
                    }
                    break;
            }
        }
    }
    void BallToBeforeSet()
    {
        transform.position = new Vector3(1f, 7f, -0.5f);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        SwipeSet.secondTouch = false;
        SwipeSet.secondTouchAllowed = true;
        CatchBall.enabled = false;
        CatchBall.spikable = false;
    }
    void ResetSlowMo()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }
}