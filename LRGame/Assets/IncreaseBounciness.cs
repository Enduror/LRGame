using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseBounciness : MonoBehaviour
{
    private void OnCollisionExit(Collision collision)
    {
        GetComponent<Rigidbody>().velocity *= 2;
    }
}
