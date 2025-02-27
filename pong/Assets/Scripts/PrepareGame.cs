using UnityEngine;
using Unity;
using Unity.Netcode;

public class PrepareGame : NetworkBehaviour 
{
    public delegate void GameStateChanged(GameState state);
    public event GameStateChanged OnGameStateChanged;

    private ConnectionManager connectionManager;

    private const int RequiredNbPlayers = 2;

    private int connectedPlayers = 0;

    private int readyPlayers = 0;

    public enum GameState
    {
        Waiting,
        Preparing,
        Ready,
    }

    private GameState gameState;

    public override void OnDestroy() 
    {
        connectionManager.OnNbPlayersChanged -= UpdateNbPlayers; 
        base.OnDestroy();
    }

    void Start()
    {
        connectionManager = GameObject.Find("NetworkManager").GetComponent<ConnectionManager>();
        connectionManager.OnNbPlayersChanged += UpdateNbPlayers;        
    }

    void UpdateNbPlayers(int nbPlayers)
    {
        if (IsOwner)
        {
            Debug.Log("MK: --------------------- UpdateNbPlayers " + nbPlayers);
            connectedPlayers = nbPlayers;
            if (connectedPlayers < RequiredNbPlayers)
            {
                gameState = GameState.Waiting;
            }
            else if (connectedPlayers >= RequiredNbPlayers)
            {
                gameState = GameState.Preparing;
            }
            readyPlayers = 0;
            Debug.Log("MK: --------------------- UpdateNbPlayers " + gameState);
            // important to send the state as this variable is local on each client
            // its change shall be communicated to everyone. 
            UpdateGameStatusRpc(gameState);
        }
    }

    [Rpc(SendTo.Owner)]
    public void NotifyPlayerReadyRpc()
    {
        Debug.Log("MK: --------------------- NotifyPlayerReady " + readyPlayers);
        gameState = GameState.Ready;
        UpdateGameStatusRpc(gameState);

        readyPlayers++;
        if (readyPlayers == RequiredNbPlayers)
        {
            LoadScene();
        }
    }

    [Rpc(SendTo.Everyone)]
    private void UpdateGameStatusRpc(GameState newState)
    {
        Debug.Log("MK: --------------------- UpdateGameStatusRpc " + newState);
        OnGameStateChanged?.Invoke(newState);
    }

    public void LoadScene()
    {
        Debug.Log("MK: --------------------- LoadSceneRpc owner loads scene ");
        NetworkManager.Singleton.SceneManager.LoadScene("GameScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }    

}