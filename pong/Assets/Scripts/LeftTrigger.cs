using UnityEngine;

public class LeftTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().PlayerRightScored();
    }
}
