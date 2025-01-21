using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;


public class MovementPlayer : MonoBehaviour
{
    Rigidbody2D rb;
    InputActionMap am1;
    public float movespeed = 0;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        am1 = GetComponent<PlayerInput>().currentActionMap;
    }

    private void Update()
    {
        rb.linearVelocity = am1.FindAction("Move").ReadValue<Vector2>() * movespeed;
    }

}
