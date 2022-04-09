using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilServeManager : MonoBehaviour
{

    public GameObject ball;
    public Vector3 ServePosition;
    public float ServeStrength;
    public float XServeVariation;
    public float YServeVariation;
    public float ZServeVariation;
    public float ServeHeight;
    public CameraAttachedToPlayer cam;

    public GameObject[] Receiver;

    public Vector3 serveDirection;
    public Vector3 RandomizedDirection;
    public float StrengthVariation;

    public float timer;

    public float timeToServe;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ServeBall());
    }



    IEnumerator ServeBall()
    {
        while (true)
        {
            
            cam.ResetCam();

            var currentReceiver = Receiver[Random.Range(0, Receiver.Length)];
            currentReceiver.GetComponent<MobilePlayerController>().enabled = true;
            cam.AttachToReceiver(currentReceiver);
            serveDirection = currentReceiver.transform.position - transform.position;
            serveDirection = new Vector3(serveDirection.x + Random.Range(-XServeVariation, XServeVariation), serveDirection.y + ServeHeight + Random.Range(-YServeVariation, YServeVariation), serveDirection.z + Random.Range(-ZServeVariation, ZServeVariation));



            float finalStrength = ServeStrength + Random.Range(0, StrengthVariation);


            GameObject go = Instantiate(ball, transform.position, Quaternion.identity);
            go.GetComponent<Rigidbody>().useGravity = true;
            go.GetComponent<Rigidbody>().AddForce(
              serveDirection.normalized * finalStrength);
            Destroy(go, 10);
            yield return new WaitForSeconds(timeToServe);
            currentReceiver.GetComponent<MobilePlayerController>().enabled = false;
        }
    }

}

