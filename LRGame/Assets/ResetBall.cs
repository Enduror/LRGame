using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBall : MonoBehaviour
{
    Rigidbody rb;
    public int TapCount;
    bool hitBall = false;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
                            hitBall = true;
                        }
                    }
                    break;

                case TouchPhase.Ended:
                    if (hitBall == false)
                    {
                        TapCount++;
                    }
                    if (TapCount == 2)
                    {
                        rb.velocity = Vector3.zero;
                        rb.angularVelocity = Vector3.zero;
                        rb.position = new Vector3(0f, 7f, -0.5f);
                        Time.timeScale = 1;
                        Time.fixedDeltaTime = 0.02F * Time.timeScale;
                    }
                    break;
            }
        }
    }
}