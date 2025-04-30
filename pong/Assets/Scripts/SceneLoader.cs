using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SceneLoader : NetworkBehaviour
{
  public void LoadGameScene()
  {
    // load the menu scence
    NetworkManager.Singleton.SceneManager.LoadScene("GameScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
  }

  public void LoadSessionScene()
  {
    FindFirstObjectByType<NetworkManager>().SceneManager.LoadScene("SessionScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
    // modify gui for owner
    NetworkManager.Singleton.SceneManager.OnSceneEvent += HandleSceneEvent;
  }

  void HandleSceneEvent(SceneEvent sceneEvent)
  {
    if (sceneEvent.SceneEventType == SceneEventType.LoadComplete)
    {
      GameObject.Find("StartButton").GetComponent<Button>().interactable = true;
      GameObject.Find("StartButton").GetComponentInChildren<TMP_Text>().text = "Start";

      // delay to ensures scene is fully initialized
      StartCoroutine(EnableChatWithDelay()); 
      NetworkManager.Singleton.SceneManager.OnSceneEvent -= HandleSceneEvent;
    }
  }

  IEnumerator EnableChatWithDelay()
  {
    yield return new WaitForSeconds(1); 
    EnableChatRPC();
  }

  [Rpc(SendTo.Everyone)]
  public void EnableChatRPC()
  {
    // enable chat    
    GameObject.Find("Message Input Field").GetComponent<TMP_InputField>().interactable = true;
  }

}
