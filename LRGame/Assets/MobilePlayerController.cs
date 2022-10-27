using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlayerController : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 startCameraOffset;
    private float touchSpaceY;
    private float xRotation;
    private float yRotation;
    private float zRotation;
    public float movementRange;
    public float receiveToX;

    Vector2 screenSize;
    Vector2 touchposition;
    Vector2 normalizedTouchToScreenVector;
    Vector2 adjustedVector;
    CameraFollow CameraFollow;
    private Vector3 ballPosition;
    private Vector3 AimToPosition;
    private Vector3 resultVector;


    // Start is called before the first frame update
    void Start()
    {
        screenSize = new Vector2(Screen.width, Screen.height);
        startPosition = transform.position;
        CameraFollow = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();
        startCameraOffset = CameraFollow.offset;
        touchSpaceY = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchposition = touch.position;

            // movement------------------------------
            normalizedTouchToScreenVector = new Vector2(
                touchposition.x / screenSize.x, touchposition.y / screenSize.y);        // x,y=[0,1]
            // adjust touch input to player position on screen
            adjustedVector = normalizedTouchToScreenVector + new Vector2(-0.5f, 0);    // x=[-0.5,0.5]
            if (adjustedVector.y > touchSpaceY)
            {
                adjustedVector.y = touchSpaceY;                                         // y=[0,touchSpaceY]
            }
            if (adjustedVector.y < 0.2f)
            {
                adjustedVector.y = 0;                                         // y=[0.2,touchSpaceY]
            }
            // move player
            transform.position = startPosition + new Vector3(adjustedVector.x, 0, adjustedVector.y) * movementRange;
            // move player
            CameraFollow.offset = startCameraOffset + new Vector3(-2*adjustedVector.x, touchSpaceY - adjustedVector.y, -adjustedVector.y);

            // player arm rotation-------------------
            // current position of the ball in inertial system
            ballPosition = GameObject.FindWithTag("Ball").GetComponent<Transform>().position;
            // vector: player to ball
            ballPosition = ballPosition - transform.position;
            // vector: player to aiming point
            AimToPosition = new Vector3(receiveToX, 18, 1) - transform.position; //y: ball speed dependent 
            resultVector = ballPosition + AimToPosition;
            // align player board to aming point
            transform.LookAt(resultVector);
            transform.Rotate(90, 0, 0);
            transform.Rotate(0, adjustedVector.x * 180, 0);
        }


    }
}
