using UnityEngine;

public class RightTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        GameObject.Find("GameManager").GetComponent<GameManagerScript>().LeftPlayerScored();
    }
}
