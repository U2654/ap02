using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;


public class PaddleScript : MonoBehaviour
{
    public PaddleObserver paddleObserver = new PaddleObserver();

    Rigidbody2D rb;
    InputActionMap am;
    public float speed = 0;

    private void Start() 
    {
        paddleObserver.paddleScript = this;
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

public class PaddleObserver : ObserverPattern.Observer
{
    public PaddleScript paddleScript;
    public override void Update()
    {
        paddleScript.ChangeColor();
    }
}