using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private ServerSettings _serverSettings;
    [SerializeField] private TMP_Text _stateUiText;

    private LoadBalancingClient _lbc;

    [SerializeField] private string _region;
    [SerializeField] private string _sceneName = "Game";

    [SerializeField] private TMP_InputField _roomName;
    [SerializeField] private Button _createRoom;
    [SerializeField] private Button _joinRandomRoom;
    [SerializeField] private Button _joinRoom;

    [Header("Room List Panel")]
    [SerializeField] private ListItem _itemPrefab;
    [SerializeField] private Transform _content;

    List<RoomInfo> _roomInfos = new List<RoomInfo>();

    void Start()
    {

        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.ConnectToRegion(_region);

        _createRoom.onClick.AddListener(CreateRoom);
        _joinRandomRoom.onClick.AddListener(JoinRandomRoom);
        _joinRoom.onClick.AddListener(JoinRoomButton);

        DontDestroyOnLoad(this.gameObject);
    }


    void Update()
    {
        var state = PhotonNetwork.NetworkClientState.ToString();
        _stateUiText.text = state;
    }

    public override void OnConnectedToMaster()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();    
        }
    }

    public void CreateRoom()
    {
        Debug.Log("CreateRoom");
        if (!PhotonNetwork.IsConnected)
            return;

        var roomOptions = new RoomOptions
        {
            MaxPlayers = 12,
            PublishUserId = true,
        };
        PhotonNetwork.CreateRoom(_roomName.text, roomOptions, TypedLobby.Default);
        PhotonNetwork.LoadLevel(_sceneName);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log(" Create Room Failed");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var info in roomList)
        {
            ListItem listItem = Instantiate(_itemPrefab, _content);
            if (listItem != null)
            {
                listItem.SetInfo(info);
            }
        }

    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(_sceneName);
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    } 
    public void JoinRoomButton()
    {
        PhotonNetwork.JoinRoom(_roomName.text);
    }

}
