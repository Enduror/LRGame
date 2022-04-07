using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilBallHandler : MonoBehaviour
{
    public Vector3[] receiveVectors;
    public Vector3 receiveVector6;
    public float receiveForce;

    public GameObject marker;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().AddForce(receiveVector6.normalized * receiveForce);
        }
        if (collision.gameObject.tag == "Field")
        {
            Instantiate(marker, transform.position, Quaternion.identity);
        }
    }
}
