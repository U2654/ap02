using System;
using UnityEngine;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Multiplayer;

public class ConnectionManager : MonoBehaviour
{
    [SerializeField]
    int m_MaxPlayers = 2;

    public delegate void NbPlayersChanged(int newValue);
    public event NbPlayersChanged OnNbPlayersChanged;

    public ConnectionState State { get; private set; } = ConnectionState.Disconnected;

    public enum ConnectionState
    {
        Disconnected,
        Connecting,
        Connected,
    }

    NetworkManager m_NetworkManager;

    public static ISession m_Session;

    async void Awake()
    {
        m_NetworkManager = FindFirstObjectByType<NetworkManager>();
        m_NetworkManager.OnSessionOwnerPromoted += OnSessionOwnerPromoted;
        m_NetworkManager.OnClientConnectedCallback += OnClientConnectedCallback;
        await UnityServices.InitializeAsync();
    }


    public async void Disconnect()
    {
        Debug.Log("MK: --------------------- Leaving Session");
        if (m_Session != null)
        {
            await m_Session.LeaveAsync();
        }
        State = ConnectionState.Disconnected;
        OnNbPlayersChanged?.Invoke(m_Session.PlayerCount);
    }


    public async Task CreateOrJoinSessionAsync(string sessionName, string profileName)
    {
        if (string.IsNullOrEmpty(profileName) || string.IsNullOrEmpty(sessionName))
        {
            Debug.LogError("Please provide a player and session name, to login.");
            return;
        }

        State = ConnectionState.Connecting;
        try
        {
            // Only sign in if not already signed in.
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                AuthenticationService.Instance.SwitchProfile(profileName);
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }

            // Set the session options.
            var options = new SessionOptions()
            {
                Name = sessionName,
                MaxPlayers = m_MaxPlayers
            }.WithDistributedAuthorityNetwork();

            // Join a session if it already exists, or create a new one.
            m_Session = await MultiplayerService.Instance.CreateOrJoinSessionAsync(sessionName, options);
            State = ConnectionState.Connected;
        }
        catch (Exception e)
        {
            State = ConnectionState.Disconnected;
            Debug.LogException(e);
        }
    }

    void OnClientConnectedCallback(ulong clientId)
    {
        if (m_NetworkManager.LocalClientId == clientId)
        {
            Debug.Log($"Client-{clientId} is connected and can spawn {nameof(NetworkObject)}s.");
        }
        Debug.Log("MK: --------------------- number clients "+m_Session.PlayerCount);
        if (m_NetworkManager.LocalClient.IsSessionOwner)
        {
            OnNbPlayersChanged?.Invoke(m_Session.PlayerCount);
        }
    }

    // Just for logging.
    void OnSessionOwnerPromoted(ulong sessionOwnerPromoted)
    {
        if (m_NetworkManager.LocalClient.IsSessionOwner)
        {
            Debug.Log($"Client-{m_NetworkManager.LocalClientId} is the session owner!");
        }
    }
}

