using UnityEngine;
using Unity.Netcode;
public class DisconnectButton : MonoBehaviour
{
    public void OnButtonPressed()
    {
        NetworkManager.Singleton.GetComponent<ConnectionManager>().Disconnect();
    }
}
