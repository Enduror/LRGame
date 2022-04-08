using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAttachedToPlayer : MonoBehaviour
{
    public Transform playerObject;
    public Vector3 Offset;
    public Vector3 PositionBeforeReceive;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = PositionBeforeReceive;
    }  

    public void AttachToReceiver(GameObject Receiver)
    {
        transform.SetParent(Receiver.transform);
        transform.position = Receiver.transform.position + Offset;
        
    }
    public void ResetCam()
    {
        transform.position = PositionBeforeReceive;
    }
}
