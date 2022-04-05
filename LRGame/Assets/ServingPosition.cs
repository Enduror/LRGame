using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServingPosition : MonoBehaviour
{
    public GameObject Ball;
    bool FloatServe = false;
    public float FloatHitSkill = 2f;
    public float TennisHitSkill = 10f;

    // Start is called before the first frame update
    void Start()
    {
        float Xstart = Xrandom();
        Vector3 ServingPlayerPosition = new Vector3(Xstart, 1f, 10f);
        transform.position = ServingPlayerPosition;
        InitializeBall(ServingPlayerPosition);
        
    }


    // Update is called once per frame
    void Update()
    {
        // input to position serving player
        if (Input.GetKeyDown("p")) 
        {
            float Xstart = Xrandom();
            Vector3 ServingPlayerPosition = new Vector3(Xstart, 1f, 10f);
            transform.position = ServingPlayerPosition;
            InitializeBall(ServingPlayerPosition);
        }

        // input to execute tennis serve
        if (Input.GetKeyDown("o"))
        {
            // inital flight direction
            GameObject.Find("BallWithTexture(Clone)").GetComponent<Transform>().position = transform.position + new Vector3(0, 4, 0);
            GameObject.Find("BallWithTexture(Clone)").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            float YrandomStartVelocity = Random.Range(-4f, 0f);
            float ZrandomStartVelocity = Random.Range(22f, 30f);
            GameObject.Find("BallWithTexture(Clone)").GetComponent<Rigidbody>().velocity = new Vector3(0f, YrandomStartVelocity, -ZrandomStartVelocity);

            // inital angularVelocity

            float XrandomStartAngularVelocity = Random.Range(TennisHitSkill, 2 * TennisHitSkill);
            float YrandomStartAngularVelocity = Random.Range(TennisHitSkill / 2, TennisHitSkill);
            GameObject.Find("BallWithTexture(Clone)").GetComponent<Rigidbody>().angularVelocity = new Vector3(-XrandomStartAngularVelocity, -YrandomStartAngularVelocity, 0f);
            FloatServe = false;

        }
        // input to execute float serve
        if (Input.GetKeyDown("i"))
        {
            FloatServe = true;
            // inital flight direction
            GameObject.Find("BallWithTexture(Clone)").GetComponent<Transform>().position = transform.position + new Vector3(0, 4, 0);
            GameObject.Find("BallWithTexture(Clone)").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            // GameObject.Find("BallWithTexture(Clone)").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            float YrandomStartVelocity = Random.Range(0f, 3f);
            float ZrandomStartVelocity = Random.Range(17f, 22f);
            GameObject.Find("BallWithTexture(Clone)").GetComponent<Rigidbody>().velocity = new Vector3(0f, YrandomStartVelocity, -ZrandomStartVelocity);

            // inital angularVelocity
            float XrandomStartAngularVelocity = Random.Range(-FloatHitSkill, FloatHitSkill);
            float YrandomStartAngularVelocity = Random.Range(-FloatHitSkill, FloatHitSkill);
            float ZrandomStartAngularVelocity = Random.Range(-FloatHitSkill, FloatHitSkill);
            GameObject.Find("BallWithTexture(Clone)").GetComponent<Rigidbody>().angularVelocity = new Vector3(XrandomStartAngularVelocity, YrandomStartAngularVelocity, ZrandomStartAngularVelocity);


        }

        if (FloatServe)
        {
            Vector3 CurrentVelocity = GameObject.Find("BallWithTexture(Clone)").GetComponent<Rigidbody>().velocity;
            float XrandomVelocity = Random.Range(-0.05f, 0.05f);
            float YrandomVelocity = Random.Range(-0.05f, 0.05f);
            GameObject.Find("BallWithTexture(Clone)").GetComponent<Rigidbody>().velocity = CurrentVelocity + new Vector3(XrandomVelocity, YrandomVelocity, 0f);

            //check position 
            Vector3 CurrentPosition = GameObject.Find("BallWithTexture(Clone)").GetComponent<Transform>().position;
            if (CurrentPosition.y < 0.5)
            {
                FloatServe = false;
            }
        }
    }

    // Create random position of the serving opponent player
    float Xrandom()
    {
        float XrandomStart = (float)Random.Range(-4, 4);
        return XrandomStart;
    }

    // initialize ball above serving opponent player
    void InitializeBall(Vector3 ServingPlayerPosition)
    {
        Destroy(GameObject.Find("BallWithTexture(Clone)"));
        Vector3 ServingBallPosition = ServingPlayerPosition + new Vector3(0, 4, 0);
        Instantiate(Ball, ServingBallPosition, Quaternion.identity);
        // Ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Ball.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }


}
