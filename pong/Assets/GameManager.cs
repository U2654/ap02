using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject ball;
    public GameObject playerLeft;
    public GameObject playerRight;
    public GameObject left;
    public GameObject right;

    public GameObject textLeft;
    public GameObject textRight;

    private int scoreLeft;
    private int scoreRight;

    public void PlayerRightScored() 
    {
        scoreRight++;
        textRight.GetComponent<TextMeshProUGUI>().text = scoreRight.ToString();
        ball.GetComponent<Ball>().ResetPosition();
    }

    public void PlayerLeftScored() 
    {
        scoreLeft++;
        textLeft.GetComponent<TextMeshProUGUI>().text = scoreLeft.ToString();
        ball.GetComponent<Ball>().ResetPosition();
    }

}
