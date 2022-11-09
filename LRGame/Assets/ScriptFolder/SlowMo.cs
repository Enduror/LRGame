using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMo : MonoBehaviour
{
    Vector3 playerPosition;
    float slowMoFct;
    Rigidbody rb;
    public bool notReceived;
    // Start is called before the first frame update
    void Start()
    {
        notReceived = true;
        rb = GetComponent<Rigidbody>();
        playerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        // slow motion
        if (transform.position.z < -1f && notReceived)
        {
            slowMoFct = 0.33f + (playerPosition - transform.position).sqrMagnitude / 20f;
            if (slowMoFct > 1f)
            {
                slowMoFct = 1;
            }
            Time.timeScale = slowMoFct;
        }
        else
        {
            Time.timeScale = 1f;
            notReceived = true;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            notReceived = false;
            Time.timeScale = 1f;
        }
    }
}
