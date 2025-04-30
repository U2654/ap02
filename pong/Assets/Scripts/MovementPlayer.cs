using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class MovementPlayer : NetworkBehaviour
{
    Rigidbody2D rb;
    InputActionMap am1;
    public float movespeed = 0;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        am1 = GetComponent<PlayerInput>().currentActionMap;
    }

    public override void OnNetworkSpawn() 
    {
        if (IsSessionOwner)
        { 
            transform.position = new Vector3(-transform.position.x, transform.position.y, 0);
        }
    }

    private void FixedUpdate()
    {
        if (!HasAuthority) 
            return;
        rb.linearVelocity = am1.FindAction("Move").ReadValue<Vector2>() * movespeed;
    }

}
