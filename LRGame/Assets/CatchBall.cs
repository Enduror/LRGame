using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchBall : MonoBehaviour
{
    Rigidbody rb;
    public float playerHitAltitude = 3f;
    float slowMoStartY = 4f;
    public bool spikable = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // check if player is able to spike the ball
        if (rb.position.y > playerHitAltitude)
        {
            spikable = true;
        }

        // slowMo falling ball if player is able to spike
        if (spikable && rb.position.y < slowMoStartY && rb.velocity.y < 0 && Time.timeScale > 0.01f)
        {
            Time.timeScale = 1f - 1 / (2 * (rb.position.y - playerHitAltitude));
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
    }
}
