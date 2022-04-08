using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Vector3 offset;
    public GameObject ball;
    float xOffset;
    float zOffset;
    float yOffset;
    float yDiscrepancy;
    float yDiscrepancyOld;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - ball.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        // transform.position= ball.transform.position + offset;
        xOffset = ball.transform.position.x/3;
        if (ball.transform.position.y > 3f)
        {
            yOffset = ball.transform.position.y / 12;
            zOffset = -(ball.transform.position.y - 3f);
        }
        transform.position = new Vector3(ball.transform.position.x, 4f, -2f) + new Vector3(xOffset, yOffset, zOffset);

        //ball rises
        yDiscrepancy = ball.transform.position.y - 3f;
        if (yDiscrepancy > yDiscrepancyOld)
        {
            yOffset = (yDiscrepancy - yDiscrepancyOld) * yDiscrepancy;
            transform.LookAt(ball.transform.position - new Vector3(0f, yOffset, 0f));
        }
        //ball falls
        if (yDiscrepancy < yDiscrepancyOld)
        {
            yOffset = (yDiscrepancy - yDiscrepancyOld) * yDiscrepancy;
            transform.LookAt(ball.transform.position - new Vector3(0f, -yOffset, 0f));
        }
        yDiscrepancyOld = yDiscrepancy;
        //transform.LookAt(ball.transform.position - new Vector3(0f, -2 * yOffset, 0f));
    }
}