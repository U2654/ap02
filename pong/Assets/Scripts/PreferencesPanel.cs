using UnityEngine;

public class PreferencesPanel : MonoBehaviour
{

    private PrepareGame prepareGame;

    void Start()
    {
        gameObject.SetActive(false);
        prepareGame = GameObject.Find("MenuCanvas").GetComponent<PrepareGame>();
        prepareGame.OnGameStateChanged += UpdatePanel;                
    }

    public void OnDestroy()
    {
        prepareGame.OnGameStateChanged -= UpdatePanel;        
    }

    private void UpdatePanel(PrepareGame.GameState state)
    {
        if (state == PrepareGame.GameState.Preparing)
        {
            gameObject.SetActive(true);
        }        
    }

}
