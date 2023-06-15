using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Server : MonoBehaviour
{
    private const int MAX_CONNECTION = 10;
    private int port = 5805;
    private int hostID;
    private int reliableChannel;
    private bool isStarted = false;
    private byte error;
    [SerializeField] private UIController controller;
           
    List<int> connectionIDs = new List<int>();
    Dictionary<int, string> dictionary = new Dictionary<int, string>();   

    public void StartServer()
    {
        NetworkTransport.Init();
        ConnectionConfig cc = new ConnectionConfig();
        reliableChannel = cc.AddChannel(QosType.Reliable);
        HostTopology topology = new HostTopology(cc, MAX_CONNECTION);
        hostID = NetworkTransport.AddHost(topology, port);

        isStarted = true;
    }

    public void ShutDownServer()
    {
        if (!isStarted) return;

        NetworkTransport.RemoveHost(hostID);
        NetworkTransport.Shutdown();
        isStarted = false;
    }

    public void SendMessage(string message, int connectionID)
    {
        byte[] buffer = Encoding.Unicode.GetBytes(message);
        NetworkTransport.Send(hostID, connectionID, reliableChannel, buffer, message.Length * sizeof(char), out error);

        if((NetworkError)error != NetworkError.Ok) Debug.Log((NetworkError)error);
    }

    public void SendMessageToAll(string message)
    {
        for(int i = 0; i < connectionIDs.Count; i++)
        {
            SendMessage(message, connectionIDs[i]);
        }
    }

    private void Update()
    {
        if (isStarted) return;
                
        int recHostID;
        int connectionID;
        int channelID;
        byte[] recBuffer = new byte[1024];
        int bufferSize = 1024;
        int dataSize;
        NetworkEventType recData = NetworkTransport.Receive(out recHostID, out connectionID,
            out channelID, recBuffer, bufferSize, out dataSize, out error);

        while (recData != NetworkEventType.Nothing)
        {
            switch(recData)
            { 

                case NetworkEventType.Nothing:
                    break;

                case NetworkEventType.ConnectEvent:
                    connectionIDs.Add(connectionID);
                    byte[] nameBuff = Encoding.Unicode.GetBytes(controller.UserName);
                    controller.UserName = Encoding.Unicode.GetString(nameBuff, 0, dataSize);
                    dictionary.Add(connectionID, controller.UserName);
                    SendMessageToAll($"{connectionID}");
                    SendMessageToAll($"Player {controller.UserName} has connected.");
                    Debug.Log($"Player {connectionID} has connected.");

                    break;

                case NetworkEventType.DataEvent:
                    string message = Encoding.Unicode.GetString(recBuffer, 0, dataSize);
                    SendMessageToAll($"Player {connectionID}: {message}");
                    Debug.Log($"Player {connectionID}: {message}");
                    break;

                case NetworkEventType.DisconnectEvent:
                    connectionIDs.Remove(connectionID);

                    SendMessageToAll($"Player {connectionID} has disconnected.");
                    Debug.Log($"Player {connectionID} has disconnected.");
                    break;

                case NetworkEventType.BroadcastEvent:
                    break;
            }

            recData = NetworkTransport.Receive(out recHostID, out connectionID,
            out channelID, recBuffer, bufferSize, out dataSize, out error);
        }
    }
}
