using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;


public class Launcher : MonoBehaviourPunCallbacks
{
    [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so new room will be created")]
    [SerializeField]
    private byte maxPlayersPerRoom = 4;
    [Tooltip("The Ui Panel to let the user enter name, connect and play")]
    [SerializeField]
    private GameObject controlPanel;
    [Tooltip("The UI Label to inform the user that the connection is in progress")]
    [SerializeField]
    private GameObject progressLabel;
    [SerializeField]
    private GameObject roomListPanel;
    public InputField roomNameInput;

    //bool isConnecting;

    string gameVersion = "1";


    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    void Start()
    {
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        roomListPanel.SetActive(false);
    }


    public void Connect()
    {

        progressLabel.SetActive(true);
        controlPanel.SetActive(false);

        // if (PhotonNetwork.IsConnected)
        // {
        //     //PhotonNetwork.JoinRandomRoom();
        //     progressLabel.SetActive(false);
        //     roomListPanel.SetActive(true);
        // }
        // else
        // {

        //     isConnecting = true;
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
        //}
    }

    public override void OnConnectedToMaster()
    {
        // if (isConnecting)
        // {
        PhotonNetwork.JoinLobby();
        //isConnecting = false;
        controlPanel.SetActive(false);
        progressLabel.SetActive(false);
        roomListPanel.SetActive(true);

        //}
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        //isConnecting = false;
        progressLabel.SetActive(false);
        controlPanel.SetActive(true);
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }

    public override void OnJoinedRoom()
    {

        PhotonNetwork.LoadLevel("Game");

        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
    }

    public void JoinOrCreatRoom()
    {
        if (roomNameInput.text.Length < 2)
            return;

        RoomOptions options = new RoomOptions { MaxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom(roomNameInput.text, options, default);
    }
}
