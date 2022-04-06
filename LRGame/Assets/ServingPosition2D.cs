using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingPosition2D : MonoBehaviour
{
    public GameObject Ball2D;
    GameObject BallCircle;
    // Start is called before the first frame update
    void Start()
    {
        SetServePosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetServePosition()
    {
        Destroy(GameObject.Find("BallCircle(Clone)"));
        // Player position
        float XrandomStart = (float)Random.Range(-4, 4);
        Vector3 ServingPlayerPosition = new Vector3(XrandomStart, 1f, 10f);
        transform.position = ServingPlayerPosition;
        // Ball initialized and placed above player
        Vector3 ServingBallPosition = ServingPlayerPosition + new Vector3(0, 2, 0);
        Instantiate(Ball2D, ServingBallPosition, Quaternion.identity);
        Ball2D.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
