using UnityEngine;
using UnityEngine.SceneManagement;

// The InitNetworkScene with this script exists because 
// the NetworkManager shall only be created once.
// Changing to a scene with a NetworkManager object will create 
// the object every time. This shall be avoided.
public class LoadMenuScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // load the menu scence
        SceneManager.LoadScene("MenuScene");               
    }

}
