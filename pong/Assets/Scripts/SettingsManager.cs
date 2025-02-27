using UnityEngine;
using Unity;
using Unity.Netcode;

public class SettingsManager : NetworkBehaviour 
{
   // Define  events to notify UI scripts
    public delegate void SpeedValueChanged(float newValue);
    public event SpeedValueChanged OnSpeedValueChanged;

    public delegate void WinScoreValueChanged(int newValue);
    public event WinScoreValueChanged OnWinScoreValueChanged;


    private float speedValue = 2.0f;
    private int winScoreValue = 1;

    private PrepareGame prepareGame;

    void Start()
    {
        prepareGame = GameObject.Find("MenuCanvas").GetComponent<PrepareGame>();
        prepareGame.OnGameStateChanged += GameStateNotify;        
    }

    private void GameStateNotify(PrepareGame.GameState state)
    {
        if (state == PrepareGame.GameState.Preparing)
        {
            // Initialize the slider values on all clients
            UpdateSpeedValueRpc(speedValue);
            UpdateWinScoreValueRpc(winScoreValue);
        }
        if (state == PrepareGame.GameState.Ready)
        {
            Debug.Log("MK: --------------------- received game state " + state);
            Debug.Log("MK: --------------------- prefs winscore " + winScoreValue + " speed " + speedValue);

            PlayerPrefs.SetInt("WinScore", winScoreValue);  
            PlayerPrefs.SetFloat("BallSpeed", speedValue);  
            PlayerPrefs.Save();
        }
    }


    [Rpc(SendTo.Owner)]
    public void SetSpeedValueRpc(float newValue)
    {
        Debug.Log("MK: --------------------- rpc call set received: ball speed:  " + newValue );
        UpdateSpeedValueRpc(newValue);
    }

    [Rpc(SendTo.Everyone)]
    private void UpdateSpeedValueRpc(float newValue)
    {
        Debug.Log("MK: --------------------- rpc call update received: speed " + newValue);
        speedValue = newValue;
        // Trigger the event to notify UI scripts
        OnSpeedValueChanged?.Invoke(newValue);
    }

    [Rpc(SendTo.Owner)]
    public void SetWinScoreValueRpc(int newValue)
    {
        Debug.Log("MK: --------------------- rpc call set received: win score  " + newValue );
        UpdateWinScoreValueRpc(newValue);
    }

    [Rpc(SendTo.Everyone)]
    private void UpdateWinScoreValueRpc(int newValue)
    {
        winScoreValue = newValue;
        Debug.Log("MK: --------------------- rpc call update received: win score " + newValue);
        // Trigger the event to notify UI scripts
        OnWinScoreValueChanged?.Invoke(newValue);
    }

}    
