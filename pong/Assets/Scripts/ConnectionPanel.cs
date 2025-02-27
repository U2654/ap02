using UnityEngine;
using Unity.Netcode;
using TMPro;
using System.Linq;
public class ConnectionPanel : MonoBehaviour
{

    public TMP_InputField sessionNameField;
    public TMP_InputField playerNameField;
    ConnectionManager connectionManager;

    public void OnConnectButtonClick() 
    {
        if (sessionNameField.text.Length > 0 && playerNameField.text.Length > 0)
        {
            connectionManager = NetworkManager.Singleton.GetComponent<ConnectionManager>();
            _ = connectionManager.CreateOrJoinSessionAsync(sessionNameField.text, playerNameField.text); 
        }
    }
}
