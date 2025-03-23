using UnityEngine;

public class BallScript : MonoBehaviour
{
    public BallSubject ballSubject = new BallSubject();

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
        ballSubject.Notify();
    }
}

public class BallSubject : ObserverPattern.Subject
{
    // could provide something specific
}



