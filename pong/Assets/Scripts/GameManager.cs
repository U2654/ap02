using TMPro;
using UnityEngine;
using Unity.Netcode;
using System.Collections;

public class GameManager : NetworkBehaviour
{
    public GameObject ballPrefab;
    public GameObject ball;

    public GameObject left;
    public GameObject right;

    public GameObject textLeft;
    public GameObject textRight;

    private NetworkVariable<int> scoreLeft = new(0);
    private NetworkVariable<int> scoreRight = new(0);

    public override void OnNetworkSpawn() 
    {
        if (IsServer)
        {
            ball = Instantiate(ballPrefab);
            ball.GetComponent<NetworkObject>().Spawn();
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
        }
        scoreRight.OnValueChanged += UpdateScore;
        scoreLeft.OnValueChanged += UpdateScore;
    }

    private void OnClientConnected(ulong clientId)
    {
        if (NetworkManager.Singleton.ConnectedClients.Count > 1)
        {
            ball.GetComponent<Ball>().ResetPosition();
        }
    }    

    private void OnClientDisconnected(ulong clientId)
    {
        if (NetworkManager.Singleton.ConnectedClients.Count <= 1)
        {
            scoreRight.Value  = 0;
            scoreLeft.Value = 0;
            ball.GetComponent<Ball>().Stop();
        }
    }    


    private void UpdateScore(int previous, int current)
    {
        textRight.GetComponent<TextMeshProUGUI>().text = scoreRight.Value.ToString();
        textLeft.GetComponent<TextMeshProUGUI>().text = scoreLeft.Value.ToString();
    }


    public void PlayerRightScored() 
    {
        if (IsServer)
        {
            scoreRight.Value++;
        }
        ball.GetComponent<Ball>().ResetPosition();
    }

    public void PlayerLeftScored() 
    {
        if (IsServer)
        {
            scoreLeft.Value++;
        }
        ball.GetComponent<Ball>().ResetPosition();
    }

}
