using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerScript : MonoBehaviour
{

    PlayerController controls;
    Vector2 move;

    // Start is called before the first frame update

    private void Awake()
    {
        controls = new PlayerController();
        controls.GamePlay.Movement.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.GamePlay.Movement.canceled +=ctx=> move = Vector2.zero;

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 m = new Vector2(-move.x, move.y) * Time.deltaTime;
        transform.Translate(m, Space.World);
    }      

    private void OnEnable()
    {
        controls.GamePlay.Enable();
    }
}
