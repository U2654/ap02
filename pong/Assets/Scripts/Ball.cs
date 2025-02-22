using UnityEngine;
using Unity.Netcode;

public class Ball : NetworkBehaviour
{
    public float speed = 0f;
    public Rigidbody2D rb;

    public AudioSource collisionSound;

    void Start()
    {
        speed = PlayerPrefs.GetFloat("BallSpeed");    
        ResetPosition();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (IsServer)
        {
            PlayOnClientRPC();
        }
    }

    [ClientRpc]
    public void PlayOnClientRPC() 
    {
        collisionSound.Play();
    }

    public void ResetPosition()
    {
        transform.position = Vector3.zero;
        float x = Random.Range(0,2) == 0 ? -1 : 1;
        float y = Random.Range(0,2) == 0 ? -1 : 1;
        rb.linearVelocity = new Vector2(speed*x, speed*y);
    }

    public void Stop()
    {
        transform.position = Vector3.zero;
        rb.linearVelocity = Vector2.zero;
    }

}
