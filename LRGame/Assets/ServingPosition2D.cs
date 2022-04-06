using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingPosition2D : MonoBehaviour
{
    public GameObject Ball2D;
    GameObject BallCircle;
    bool FloatServe = false;
    bool TennisServe = false;
    public float FloatHitSkill = 2f;
    public float TennisHitSkill = 10f;

    // Start is called before the first frame update
    void Start()
    {
        SetServePosition();
    }

    // Update is called once per frame
    void Update()
    {
        // input to set the serving position of player and ball
        if (Input.GetKeyDown("p"))
        {
            SetServePosition();
        }

        if (Input.GetKeyDown("o"))
        {
            TennisServe = true;
            FloatServe = false;
            // inital flight direction
            GameObject.Find("BallWithTexture(Clone)").GetComponent<Transform>().position = transform.position + new Vector3(0, 2, 0);
            GameObject.Find("BallWithTexture(Clone)").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GameObject.Find("CircleBall(Clone)").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            float YrandomStartVelocity = Random.Range(2.5f, 4.5f);
            float ZrandomStartVelocity = Random.Range(23f, 27f);
            GameObject.Find("BallWithTexture(Clone)").GetComponent<Rigidbody>().velocity = new Vector3(0f, YrandomStartVelocity, -ZrandomStartVelocity);

            // inital angularVelocity
            float XrandomStartAngularVelocity = Random.Range(TennisHitSkill, 2 * TennisHitSkill);
            float YrandomStartAngularVelocity = Random.Range(TennisHitSkill / 2, TennisHitSkill);
            GameObject.Find("BallWithTexture(Clone)").GetComponent<Rigidbody>().angularVelocity = new Vector3(-XrandomStartAngularVelocity, -YrandomStartAngularVelocity, 0f);

        }

        // input to execute float serve
        if (Input.GetKeyDown("i"))
        {
            FloatServe = true;
            TennisServe = false;
            // inital flight direction
            GameObject.Find("CircleBall(Clone)").GetComponent<Transform>().position = transform.position + new Vector3(0, 2, 0);
            GameObject.Find("CircleBall(Clone)").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GameObject.Find("CircleBall(Clone)").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            float YrandomStartVelocity = Random.Range(3f, 5f);
            float ZrandomStartVelocity = Random.Range(20f, 23f);
            GameObject.Find("CircleBall(Clone)").GetComponent<Rigidbody>().velocity = new Vector3(0f, YrandomStartVelocity, -ZrandomStartVelocity);
            // inital angularVelocity
            float XrandomStartAngularVelocity = Random.Range(-FloatHitSkill, FloatHitSkill);
            float YrandomStartAngularVelocity = Random.Range(-FloatHitSkill, FloatHitSkill);
            float ZrandomStartAngularVelocity = Random.Range(-FloatHitSkill, FloatHitSkill);
            GameObject.Find("CircleBall(Clone)").GetComponent<Rigidbody>().angularVelocity = new Vector3(XrandomStartAngularVelocity, YrandomStartAngularVelocity, ZrandomStartAngularVelocity);
        }

        if (FloatServe)
        {
            Vector3 CurrentVelocity = GameObject.Find("CircleBall(Clone)").GetComponent<Rigidbody>().velocity;
            float XrandomVelocity = Random.Range(-0.05f, 0.05f);
            float YrandomVelocity = Random.Range(-0.05f, 0.05f);
            GameObject.Find("CircleBall(Clone)").GetComponent<Rigidbody>().velocity = CurrentVelocity + new Vector3(XrandomVelocity, YrandomVelocity, 0f);

            //check position 
            Vector3 CurrentPosition = GameObject.Find("CircleBall(Clone)").GetComponent<Transform>().position;
            if (CurrentPosition.y < 0.5)
            {
                FloatServe = false;
            }

        }

        if (TennisServe)
        {
            Vector3 CurrentVelocity = GameObject.Find("CircleBall(Clone)").GetComponent<Rigidbody>().velocity;
            // float XrandomVelocity = Random.Range(-0.05f, 0.05f);
            float YrandomVelocity = Random.Range(-0.05f, 0f);
            GameObject.Find("CircleBall(Clone)").GetComponent<Rigidbody>().velocity = CurrentVelocity + new Vector3(0f, YrandomVelocity, 0f);

            //check position 
            Vector3 CurrentPosition = GameObject.Find("CircleBall(Clone)").GetComponent<Transform>().position;
            if (CurrentPosition.y < 0.5)
            {
                FloatServe = false;
            }

        }
    }

    void SetServePosition()
    {
        Destroy(GameObject.Find("CircleBall(Clone)"));
        // Player position
        float XrandomStart = (float)Random.Range(-4, 4);
        Vector3 ServingPlayerPosition = new Vector3(XrandomStart, 1f, 10f);
        transform.position = ServingPlayerPosition;
        // Ball initialized and placed above player
        Vector3 ServingBallPosition = ServingPlayerPosition + new Vector3(0, 2, 0);
        Instantiate(Ball2D, ServingBallPosition, Quaternion.identity);
        GameObject.Find("CircleBall(Clone)").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }
}
