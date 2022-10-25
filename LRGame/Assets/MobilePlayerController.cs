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

    Vector2 screenSize;
    Vector2 touchposition;
    Vector2 normalizedTouchToScreenVector;
    Vector2 adjustedVector;
    CameraFollow CameraFollow;


    // Start is called before the first frame update
    void Start()
    {
        screenSize = new Vector2(Screen.width, Screen.height);
        startPosition = transform.position;
        CameraFollow = GameObject.FindWithTag("MainCamera").GetComponent<CameraFollow>();
        startCameraOffset = CameraFollow.offset;
        touchSpaceY = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchposition = touch.position;

            normalizedTouchToScreenVector = new Vector2(touchposition.x / screenSize.x, touchposition.y / screenSize.y); // x,y=[0,1]
            adjustedVector = normalizedTouchToScreenVector + new Vector2(-0.5f, 0f); // x=[-0.5,0.5]
            if (adjustedVector.y > touchSpaceY)
            {
                adjustedVector.y = touchSpaceY; // y=[0,touchSpaceY]
            }
            transform.position = startPosition + new Vector3(adjustedVector.x, 0, adjustedVector.y) * movementRange;
            CameraFollow.offset = startCameraOffset + new Vector3(0f, touchSpaceY - adjustedVector.y, -adjustedVector.y);

            xRotation = (0.5f - adjustedVector.y/10) * 45;
            yRotation = adjustedVector.x * 180;
            zRotation = adjustedVector.x * 180; 
            transform.rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
        }
    }
}
