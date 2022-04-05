using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReceiveScript : MonoBehaviour
{

    public float maxRotation;
    public Transform RotationPoint;
    public float rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            //RotationPoint.transform.rotation.z = new Vector3(0, 0, touchPosition.x / Screen.width);


            if (RotationPoint.rotation.z <= maxRotation && RotationPoint.rotation.z >= -maxRotation)
            {
                RotationPoint.transform.Rotate(Vector3.back, Time.deltaTime * -(touchPosition.x * rotationSpeed), Space.World);
            }
            else if (RotationPoint.rotation.z > maxRotation)
                RotationPoint.transform.rotation = Quaternion.Euler(0, 0, maxRotation*100);
            else if (RotationPoint.rotation.z < -maxRotation)
                RotationPoint.transform.rotation = Quaternion.Euler(0, 0, -maxRotation*100);
        }
    }
}
