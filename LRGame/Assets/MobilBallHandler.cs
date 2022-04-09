using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilBallHandler : MonoBehaviour
{   
    public float receiveForce;
    public float receiveHeight;
    public Vector3 receiveDirection;

    public GameObject marker;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            receiveDirection = new Vector3(-transform.position.x, receiveHeight, -transform.position.z);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().AddForce(receiveDirection.normalized * receiveForce);
        }
        if (collision.gameObject.tag == "Field")
        {
            Instantiate(marker, transform.position, Quaternion.identity);
        }
    }
}
