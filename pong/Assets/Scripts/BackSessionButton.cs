using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using TMPro;

public class BackSessionButton : MonoBehaviour
{
    void Start()
    {
        if (NetworkManager.Singleton.CurrentSessionOwner != 0 && 
            NetworkManager.Singleton.CurrentSessionOwner == NetworkManager.Singleton.LocalClientId)
        {
            // C# lambda callback registration makes problems using this.gameObject... 
            GameObject.Find("BackButton").GetComponent<Button>().interactable = true;
            GameObject.Find("BackButton").GetComponentInChildren<TMP_Text>().text = "Back to Sessions";
        }                
    }

    public void GoBack()
    {
        GameObject.Find("SceneLoader").GetComponent<SceneLoader>().LoadSessionScene();
    }


}
