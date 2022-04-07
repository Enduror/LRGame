using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlayerController : MonoBehaviour
{
    private float speedModifier;
    private Vector2 moveDirection;
    private Vector3 startPosition;
    public float movementRange;

    Vector2 screenSize;
    Vector2 normalizedTouchToScreenVector;
    Vector2 touchposition;
    // Start is called before the first frame update
    void Start()
    {
        //speedModifier = 0.01f;
        screenSize = new Vector2(Screen.width, Screen.height);
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            touchposition = touch.position;

            normalizedTouchToScreenVector =
                (new Vector2(touchposition.x / screenSize.x, touchposition.y / screenSize.y) + new Vector2(-0.5f, -0.5f)) * 2; ;

            transform.position = startPosition + new Vector3(normalizedTouchToScreenVector.x, 0, normalizedTouchToScreenVector.y) * movementRange;


        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 2);
    }

}
