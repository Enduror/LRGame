using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeSet : MonoBehaviour
{
    public Vector2 startPos;
    public Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Update the Text on the screen depending on current TouchPhase, and the current direction vector


        // Track a single touch as a direction control.
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Handle finger movements based on TouchPhase
            switch (touch.phase)
            {
                //When a touch has first been detected, change the message and record the starting position
                case TouchPhase.Began:
                    // Record initial touch position.
                    startPos = touch.position;
                    Debug.Log("Touch : Begun");
                    break;

                //Determine if the touch is a moving touch
                case TouchPhase.Moved:
                    Debug.Log("Touch : Moving");
                    break;

                case TouchPhase.Ended:
                    // Determine direction by comparing the end touch position with the initial one
                    direction = touch.position - startPos;
                    // Report that the touch has ended when it ends
                    Debug.Log("Touch : End");
                    GetComponent<Rigidbody>().useGravity = true;
                    ApplyForce();
                    break;
            }
        }
    }

    void ApplyForce()
    {
      
    }
}
