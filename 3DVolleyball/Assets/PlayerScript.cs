using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerScript : MonoBehaviour
{
    PlayerControl controls;
    public GameObject P1;
    public GameObject P2;
    // Start is called before the first frame update
    Vector2 P1movement;
    Vector2 P1rotate;

    Vector2 P2movement;
    Vector2 P2rotate;


    public float movementSpeed;
    public float rotationSpeed;

    //used for PlatformRotation.Used as pitch and jaw for Quaternion rotaion.
    float newXRotP1;
    float newZRotP1;
    float newXRotP2;
    float newZRotP2;

    //3D rotation from 2D Input
    Vector3 movementDirectionP1;
    Vector3 movementDirectionP2;

    public float jumpForce;
    private void Awake()
    {
        controls = new PlayerControl();
        controls.Player3.MovementP1.performed += ctx => P1movement = ctx.ReadValue<Vector2>();
        controls.Player3.MovementP1.canceled += ctx => P1movement = Vector2.zero;

        controls.Player3.RotateP1.performed += ctx => P1rotate = ctx.ReadValue<Vector2>();
        controls.Player3.RotateP1.canceled += ctx => P1rotate = Vector2.zero;

        controls.Player3.MovementP2.performed += ctx => P2movement = ctx.ReadValue<Vector2>();
        controls.Player3.MovementP2.canceled += ctx => P2movement = Vector2.zero;

        controls.Player3.RotateP2.performed += ctx => P2rotate = ctx.ReadValue<Vector2>();
        controls.Player3.RotateP2.canceled += ctx => P2rotate = Vector2.zero;

        //JumpP1
        controls.Player3.JumpP1.performed += ctx => JumpP1();
        controls.Player3.JumpP2.performed += ctx => JumpP2();


    }      

    private void FixedUpdate()
    {
        //Movement of Players
        Vector2 m1 = new Vector2(P1movement.x, P1movement.y);
        Vector2 m2 = new Vector2(P2movement.x, P2movement.y);

        //p1
        movementDirectionP1 = new Vector3(m1.x, 0, m1.y).normalized*movementSpeed;
        P1.transform.Translate(movementDirectionP1, Space.World);
        //p2
        movementDirectionP2 = new Vector3(m2.x, 0, m2.y).normalized * movementSpeed;        
        P2.transform.Translate(movementDirectionP2, Space.World);

        //Rotation of Players
        //P1
        Vector2 r1 = new Vector3(P1rotate.y,-P1rotate.x).normalized;        
        newXRotP1 += r1.x * Time.deltaTime * rotationSpeed;
        newZRotP1 += r1.y * Time.deltaTime * rotationSpeed;
        Transform board1 = P1.transform.GetChild(0);
        board1.rotation = Quaternion.Euler(newXRotP1,  0, newZRotP1);
       
        

        //P2
        Vector2 r2 = new Vector3(P2rotate.y, -P2rotate.x).normalized;
        newXRotP2 += r2.x * Time.deltaTime * rotationSpeed;
        newZRotP2 += r2.y * Time.deltaTime * rotationSpeed;
        Transform board2 = P2.transform.GetChild(0);        
        board2.rotation = Quaternion.Euler(newXRotP2, 0, newZRotP2);
       


    }
    void OnEnable()
    {
        controls.Player3.Enable();
    }    
    void OnDisable()
    {
        controls.Player3.Disable();
    }
    void JumpP1()
    {
        P1.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);        
    }
    void JumpP2()
    {
        P2.GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce);
    }

}
