using UnityEngine;

public class LeftWallScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        GameObject.Find("GameManager").GetComponent<GameManagerScript>().RightPlayerScored();
    }
}
