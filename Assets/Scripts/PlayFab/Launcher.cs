using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button photonDisconnect;

    private string _gameVersion = "1";

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        Connect();
    }

    public void Connect()
    {
        if(PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = _gameVersion;
        }
                
    }

    public void PhotonDisconnect()
    {
        PhotonNetwork.Disconnect();
        Debug.Log("PHOTON IS DISCONNECTED");
    }
       
    public override void OnConnectedToMaster()
    {
        Debug.Log("\"OnConnectedToMaster() was called by PUN");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Photon disconnected :" + cause);
    }
}
