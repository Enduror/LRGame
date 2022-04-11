using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBouncer : MonoBehaviour
{
    private void OnCollisionExit(Collision collision)
    {
        if(collision.transform.tag=="Player1"|| collision.transform.tag == "Player2")
        GetComponent<Rigidbody>().velocity *= 1.3f;
    }
}
