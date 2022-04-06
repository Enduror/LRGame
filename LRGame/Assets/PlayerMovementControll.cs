using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControll : MonoBehaviour
{
    public float movementSpeed;
    public float rotateSpeed;

    public CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(x, 0, z).normalized;

        controller.Move(direction * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.J))
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.L))
            transform.Rotate(-Vector3.forward* rotateSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.I))
            transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.K))
            transform.Rotate(-Vector3.right * rotateSpeed * Time.deltaTime);
               
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, transform.rotation.eulerAngles.z);



    }
}
