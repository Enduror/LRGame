using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointing : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
        transform.Rotate(10, 0, 0);
    }
}
