using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServeManager : MonoBehaviour
{
    Rigidbody rb;
    private Vector3 servePosition;
    public float timeToServe;
    private float timeRemaining;
    public Vector3 ServePosition;
    public float ServeStrength;
    public Vector3 ServeDirection;
    private Vector3 Randomization;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        servePosition = transform.position;
        timeRemaining = timeToServe;
        serveBall();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        } else 
        {
            resetBall();
            serveBall();
            timeRemaining = timeToServe;
        }
    }

    void resetBall()
    {
        transform.position = servePosition;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void serveBall() 
    {
        Randomization = new Vector3(
        ServeDirection.x + Random.Range(-0.5f, 0.5f),
        ServeDirection.y + Random.Range(-0.1f, 0.1f),
        ServeDirection.z + Random.Range(-0.0f, 0.0f));
        rb.AddForce(Randomization.normalized * ServeStrength);
    }
}
