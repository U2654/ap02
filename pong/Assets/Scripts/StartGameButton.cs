using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;

public class StartGameButton : MonoBehaviour
{

    void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback +=  NotifyJoinedSession;
    }

    public void NotifyJoinedSession(ulong clientId)
    {
        if (NetworkManager.Singleton.CurrentSessionOwner != 0 && 
            NetworkManager.Singleton.CurrentSessionOwner == NetworkManager.Singleton.LocalClientId)
        {
            // C# lambda callback registration makes problems using this.gameObject... 
            GameObject.Find("StartButton").GetComponent<Button>().interactable = true;
            GameObject.Find("StartButton").GetComponentInChildren<TMP_Text>().text = "Start";
        }        
    }

    public void StartGame()
    {
        GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadGameScene();
    }

}
