using UnityEngine;

public class BallScript : MonoBehaviour
{

    public delegate void Notify(); // define a delegate type

    public event Notify CollisionEvent; // define an event of the delegate type
    public float speed = 0f;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetPosition();
    }

    public void ResetPosition()
    {
        transform.position = Vector3.zero;
        float x = Random.Range(0,2) == 0 ? -1 : 1;
        float y = Random.Range(0,2) == 0 ? -1 : 1;
        rb.linearVelocity = new Vector2(speed*x, speed*y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CollisionEvent?.Invoke(); // notify all subscribers
    }
}

