using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchBall : MonoBehaviour
{
    Spike Spike;
    Rigidbody rb;
    float slowMoFct;
    float slowMoStartY;
    public bool spikable = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Spike = GameObject.FindWithTag("Ball").GetComponent<Spike>();
        slowMoStartY = Spike.playerHitHight + 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // check if player is able to spike the ball
        if (rb.position.y > Spike.playerHitHight)
        {
            spikable = true;
        }

        // slowMo falling ball if player is able to spike
        if (spikable && transform.position.y > Spike.playerHitHight && rb.velocity.y < 0 && Time.timeScale > 0.01f)
        {
            slowMoFct = 1f - 1 / (2 * (rb.position.y - Spike.playerHitHight + 0.5f));
            if (slowMoFct < 0.01f)
            {
                slowMoFct = 0.01f;
            }
            Time.timeScale = slowMoFct;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
        if (spikable && transform.position.y < Spike.playerHitHight - 0.5f && rb.velocity.y < 0)
        {
            slowMoFct = slowMoFct + 0.005f;
            if (slowMoFct > 1)
            {
                slowMoFct = 1;
            }
            Time.timeScale = slowMoFct;
            Time.fixedDeltaTime = 0.02F * Time.timeScale;
        }
    }
}
