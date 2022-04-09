using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCamera : MonoBehaviour
{
    public GameObject ball;
    CameraControl CameraControl;
    // Start is called before the first frame update
    void Start()
    {
        CameraControl = GameObject.FindWithTag("MainCamera").GetComponent<CameraControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RestCameraPosition()
    {
        transform.position = new Vector3(1f, 3f, -4f);
        transform.rotation = Quaternion.identity;
    }
}
