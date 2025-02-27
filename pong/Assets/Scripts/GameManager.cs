using TMPro;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System;

public class GameManager : NetworkBehaviour
{
    public GameObject ballPrefab;
    private GameObject ball;

    public GameObject left;
    public GameObject right;

    public GameObject textLeft;
    public GameObject textRight;

    public GameObject winnerPanel;

    public TextMeshProUGUI winnerText;
    private NetworkVariable<int> scoreLeft = new(0);
    private NetworkVariable<int> scoreRight = new(0);

    private int winScore;

    public override void OnNetworkSpawn() 
    {
        Debug.Log("MK: --------------------- networkmngt " + GetComponent<NetworkObject>().NetworkObjectId);
        if (IsSessionOwner)
        {
            Debug.Log("MK: --------------------- try network ball spawn");
            ball = Instantiate(ballPrefab);
            ball.GetComponent<NetworkObject>().Spawn();
        }
        winScore =  PlayerPrefs.GetInt("WinScore");  
        Debug.Log("MK: --------------------- win score is " + winScore);

        scoreRight.OnValueChanged += UpdateScore;
        scoreLeft.OnValueChanged += UpdateScore;
    }

    private void UpdateScore(int previous, int current)
    {
        textRight.GetComponent<TextMeshProUGUI>().text = scoreRight.Value.ToString();
        textLeft.GetComponent<TextMeshProUGUI>().text = scoreLeft.Value.ToString();
        CheckIfWinner();
    }


    public void PlayerRightScored() 
    {
        if (IsSessionOwner)
        {
            ball.GetComponent<Ball>().ResetPosition();
            scoreRight.Value++;
         }
    }

    public void PlayerLeftScored() 
    {
        if (IsSessionOwner)
        {
            ball.GetComponent<Ball>().ResetPosition();
            scoreLeft.Value++;
        }
    }

    private void CheckIfWinner() 
    {
        bool aWinner = false;
        String winnerString = "Winner is ";
        if (scoreLeft.Value >= winScore) 
        {
            aWinner = true;
            winnerString += "left player!";
        }
        else if (scoreRight.Value >= winScore)
        {
            aWinner = true;
            winnerString += "right player!";
        }
        if (aWinner == true)
        {
            winnerText.text = winnerString; 
            winnerPanel.SetActive(true);
            if (HasAuthority)
            {
                ball.GetComponent<Ball>().Stop();
            }
        }
    }
}
