using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject ball;
    Rigidbody rbBall;
    float xOffset;
    float zOffset;
    float yOffset;
    float BallpeakAltitude;
    SwipeSet SwipeSet;
    Spike Spike;
    Vector3 CameraInitialPosition;

    // Start is called before the first frame update
    void Start()
    {
        rbBall = ball.GetComponent<Rigidbody>();
        SwipeSet = GameObject.FindWithTag("Ball").GetComponent<SwipeSet>();
        Spike = GameObject.FindWithTag("Ball").GetComponent<Spike>();
        CameraInitialPosition = transform.position;
        BallpeakAltitude = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        // SET: after relese of touch input and foce is applied
        if (SwipeSet.secondTouchAllowed == false && Spike.thirdTouchAllowed)
        {
            // camera movement
            // ball above player's hit hight
            if (ball.transform.position.y > Spike.playerHitHight)
            {
                xOffset = ball.transform.position.x / 3;
                if (ball.transform.position.y > CameraInitialPosition.y)
                {
                    yOffset = (ball.transform.position.y - CameraInitialPosition.y) / 2;
                    if (rbBall.velocity.y > 0)
                    {
                        zOffset = -(ball.transform.position.y - CameraInitialPosition.y);
                    }
                    if (rbBall.velocity.y == 0)
                    {
                        BallpeakAltitude = ball.transform.position.y;
                    }
                    if (rbBall.velocity.y < 0)
                    {
                        //zOffset = -(ball.transform.position.y - CameraInitialPosition.y) - rbBall.velocity.y * CatchBall.playerHitAltitude / BallpeakAltitude / 6;
                        zOffset = -(ball.transform.position.y - CameraInitialPosition.y) - rbBall.velocity.y / (BallpeakAltitude - Spike.playerHitHight);
                        if (zOffset > 1f)
                        {
                            zOffset = 1f;
                        }
                    }
                }
                transform.position = new Vector3(ball.transform.position.x, CameraInitialPosition.y, CameraInitialPosition.z) + new Vector3(xOffset, yOffset, zOffset);
            }

            // camera pointing
            //ball rises
            if (rbBall.velocity.y > 6 && ball.transform.position.y > CameraInitialPosition.y)
            {
                transform.LookAt(ball.transform.position);
            }
            if (rbBall.velocity.y > 0 && rbBall.velocity.y < 6 && ball.transform.position.y > CameraInitialPosition.y)
            {
                yOffset = 1f - rbBall.velocity.y / 6;
                transform.LookAt(ball.transform.position + new Vector3(0f, -yOffset, 0f));
            }
            //ball falls
            if (rbBall.velocity.y < 0)
            {
                transform.LookAt(ball.transform.position + new Vector3(0f, -1f, 0f));
            }
        }
        // no set yet
        else
        {
            transform.rotation = Quaternion.identity;
        }

        // SPIKE: after relese of touch input and foce is applied
        if (Spike.thirdTouchAllowed == false)
        {
            // camera movement

            // camera pointing
            transform.LookAt(ball.transform.position + new Vector3(0f, -1f, 0f));

        }
    }
}