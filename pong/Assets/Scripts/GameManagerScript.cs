using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject ball;
    public GameObject leftPaddle;
    public GameObject rightPaddle;
    public GameObject scoreboard;

    public GameObject newGameButton;

    private int leftScore = 0;
    private int rightScore = 0;

    private const int maxScore = 5;

    public void Start() 
    {
        ball.GetComponent<BallScript>().ballSubject.Attach(leftPaddle.GetComponent<PaddleScript>().paddleObserver);
        ball.GetComponent<BallScript>().ballSubject.Attach(rightPaddle.GetComponent<PaddleScript>().paddleObserver);
    }

    public void NewGame() 
    {
        leftScore = 0;
        rightScore = 0;
        scoreboard.GetComponent<ScoreboardScript>().DisplayScore(leftScore, rightScore);
        scoreboard.GetComponent<ScoreboardScript>().DisplayText("");
        newGameButton.SetActive(false);
        ball.GetComponent<BallScript>().ResetPosition();
    }


    public void RightPlayerScored() 
    {
        rightScore++;
        scoreboard.GetComponent<ScoreboardScript>().DisplayScore(leftScore, rightScore);
        if (checkWinner())
        {
            scoreboard.GetComponent<ScoreboardScript>().DisplayText("Right Player Wins!");
            newGameButton.SetActive(true);
        }
        else 
        {
            ball.GetComponent<BallScript>().ResetPosition();        
        }
    }

    public void LeftPlayerScored() 
    {
        leftScore++;
        scoreboard.GetComponent<ScoreboardScript>().DisplayScore(leftScore, rightScore);
        if (checkWinner())
        {
            scoreboard.GetComponent<ScoreboardScript>().DisplayText("Left Player Wins!");
            newGameButton.SetActive(true);
        }
        else 
        {
            ball.GetComponent<BallScript>().ResetPosition();        
        }
    }

    private bool checkWinner()
    {
        if (leftScore >= maxScore || rightScore >= maxScore)
        {
            return true;
        }
        return false;
    }

}
