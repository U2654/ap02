using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleScript : MonoBehaviour
{
    Rigidbody2D rb;
    InputActionMap am;
    public float speed = 0;

    private void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        am = GetComponent<PlayerInput>().currentActionMap;
    }

    private void Update()
    {
        rb.linearVelocity = am.FindAction("Move").ReadValue<Vector2>() * speed;
    }

    public void ChangeColor()
    {
        GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
    }

}
