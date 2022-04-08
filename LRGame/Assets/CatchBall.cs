using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchBall : MonoBehaviour
{
    Rigidbody rb;
    float playerHitAltitude = 2.5f;
    float slowMoStartY = 3.5f;
    public bool spikable = false;
    public Vector3 velocity;
    public float slowRange;
    float slowDegree;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        slowRange = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity;

        // check if player is able to spike the ball
        if (transform.position.y > playerHitAltitude)
        {
            spikable = true;
        }

        // slowMo falling ball if player is able to spike
        slowDegree = 0.8f / transform.position.y;
        // ball rises -> slow down
        if (rb.velocity.y > 0 && rb.velocity.y < slowRange && spikable)
        {
            // Time.timeScale = 0.9f * rb.velocity.y / slowRange + 0.1f;
            Time.timeScale = 0.2f * rb.velocity.y / slowRange + (0.8f - slowDegree);
            Time.fixedDeltaTime = 0.02F * Time.timeScale;            
        }
        // ball falls -> accelerate
        if (rb.velocity.y < 0 && Mathf.Abs(rb.velocity.y) < slowRange && spikable)
        {
            // Time.timeScale = 0.9f * Mathf.Abs(rb.velocity.y) / slowRange + 0.1f;
            Time.timeScale = 0.2f * Mathf.Abs(rb.velocity.y) / slowRange + (0.8f - slowDegree);
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
        else
        {
            //catch ball
            if (spikable && transform.position.y < slowMoStartY && rb.velocity.y < 0 && Time.timeScale > 0.01f)
            {
                Time.timeScale = 1f - 1 / (2 * (rb.position.y - playerHitAltitude));
                Time.fixedDeltaTime = 0.02F * Time.timeScale;
            }
        }
    }

}
