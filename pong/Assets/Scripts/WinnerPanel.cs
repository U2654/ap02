using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerPanel : MonoBehaviour
{
    public void OnQuitGameButton()
    {
        Debug.Log("MK: --------------------- QUIT BUTTON --------------");
         #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif        
    }

    public void OnRestartGameButton()
    {
        Debug.Log("MK: --------------------- RESTART BUTTON --------------");
        GameObject.Find("NetworkManager").GetComponent<ConnectionManager>().Disconnect();
        SceneManager.LoadScene("MenuScene");
    }

}

