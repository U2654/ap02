using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;
using System;

public class ReadyButton : MonoBehaviour
{
    public Button readyButton;

    private PrepareGame prepareGame;

    public void Start()
    {
        prepareGame = GameObject.Find("MenuCanvas").GetComponent<PrepareGame>();
        prepareGame.OnGameStateChanged += UpdateButton;        
    }

    public void OnDestroy()
    {
        prepareGame.OnGameStateChanged -= UpdateButton;        
    }

    private void UpdateButton(PrepareGame.GameState state)
    {
        Debug.Log("MK: --------------------- update button " + state);
        switch(state)
        {
            case PrepareGame.GameState.Waiting:
                readyButton.GetComponentInChildren<TextMeshProUGUI>().text = "wait";
            break;
            case PrepareGame.GameState.Preparing:
                readyButton.GetComponentInChildren<TextMeshProUGUI>().text = "ready";
                readyButton.interactable = true;
            break;
            case PrepareGame.GameState.Ready:
                readyButton.GetComponentInChildren<TextMeshProUGUI>().text = "go";
            break;
            default:
                break;

        }
    }

    public void OnButtonClicked() 
    {
        Debug.Log("MK: --------------------- clicked button ");
        prepareGame.NotifyPlayerReadyRpc();
    }
}
