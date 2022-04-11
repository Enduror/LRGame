using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Servemanager : MonoBehaviour
{
    public GameObject ball;
    public Vector3 ServePosition;
    public float ServeStrength;

    public Vector3[] ServeDirection;
    public Vector3 ServeDirection5;
    public Vector3 Randomization;
    public float RandomizedServeStrength;


    public float timeToServe;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ServeBall(ServePosition));
    }


    IEnumerator ServeBall(Vector3 ServePosition)
    {


        while (true)
        {
            Randomization = new Vector3(
                ServeDirection5.x + Random.Range(-1f, 1f),
                ServeDirection5.y + Random.Range(-1f, 1f),
                ServeDirection5.z + Random.Range(-1f, 1f));
            RandomizedServeStrength = ServeStrength + Random.Range(-50, 50);


            GameObject go = Instantiate(ball, ServePosition, Quaternion.identity);
            go.GetComponent<Rigidbody>().useGravity = true;
            go.GetComponent<Rigidbody>().AddForce(
                Randomization.normalized * RandomizedServeStrength);
            Destroy(go, 10);

            yield return new WaitForSeconds(timeToServe);
        }
    }

}

