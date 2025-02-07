using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine.UI;
using TMPro;

public class NetworkUI : MonoBehaviour
{
    public TMP_InputField IPAddressField;    

    public void StartClient() 
    {
        string ip = IPAddressField.text;
        if (string.IsNullOrEmpty(ip))
        {
            ip = "127.0.0.1";
        }
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(ip,(ushort)7777);
        NetworkManager.Singleton.StartClient();
    }

    public void StartHost()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback = ApproveClient;
        NetworkManager.Singleton.StartHost();
    }

    private void ApproveClient(NetworkManager.ConnectionApprovalRequest request, 
                               NetworkManager.ConnectionApprovalResponse response) 
    {
        bool approveConnection = NetworkManager.Singleton.ConnectedClients.Count < 2;

        response.Approved = approveConnection;
        if (approveConnection)
        {
            response.CreatePlayerObject = true;
            response.Pending = false;
        }
    }

}
