using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    public void LoadGameSceme() 
    {
        NetworkManager.Singleton.SceneManager.LoadScene("GameScene", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

}
