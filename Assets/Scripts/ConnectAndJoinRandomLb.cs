using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConnectAndJoinRandomLb : MonoBehaviour, IConnectionCallbacks, IMatchmakingCallbacks, ILobbyCallbacks
{
    [SerializeField] private ServerSettings _serverSettings;
    [SerializeField] private TMP_Text _stateUiText;

    private LoadBalancingClient _lbc;

    private const string GAME_MODE_KEY = "gm";
    private const string AI_MODE_KEY = "ai";

    private const string MAP_PROP_KEY = "C0";
    private const string GOLD_PROP_KEY = "C1";

    //private TypedLobby _sqlLobby = new TypedLobby("customeSqlLobby", LobbyType.SqlLobby);

    void Start()
    {
        _lbc = new LoadBalancingClient();
        _lbc.AddCallbackTarget(this);

        _lbc.ConnectUsingSettings(_serverSettings.AppSettings);
    }

    private void OnDestroy()
    {
        _lbc.RemoveCallbackTarget(this);
    }

    void Update()
    {
        if (_lbc == null)
            return;
       
        _lbc.Service();

        var state = _lbc.State.ToString();
        _stateUiText.text = state;
    }
    public void OnConnected()
    {
        
    }

    public void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");



        var roomOptions = new RoomOptions
        {
            MaxPlayers = 12,
            PublishUserId = true,
            //CustomRoomPropertiesForLobby = new[] { MAP_PROP_KEY , GOLD_PROP_KEY},
            //CustomRoomProperties = new ExitGames.Client.Photon.Hashtable { {GOLD_PROP_KEY, 400 }, {MAP_PROP_KEY, "Map3" }  }
        };
        var enterRoomParams = new EnterRoomParams
        {
            RoomName = "NewRoom",
            RoomOptions = roomOptions,
            //ExpectedUsers = new[] { "3497301" },
            //Lobby = _sqlLobby
        };

        _lbc.OpCreateRoom(enterRoomParams);
        //_lbc.OpJoinRandomRoom();
    }

    public void OnCustomAuthenticationFailed(string debugMessage)
    {
        
    }

    public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
        
    }

    public void OnDisconnected(DisconnectCause cause)
    {
        
    }

    public void OnRegionListReceived(RegionHandler regionHandler)
    {
        
    }

    
    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
       
    }

    public void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
        
    }

    public void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        _lbc.CurrentRoom.IsOpen = false;
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
       
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRoomFailed");
        _lbc.OpCreateRoom(new EnterRoomParams());
    }

    public void OnLeftRoom()
    {
        
    }

    public void OnJoinedLobby()
    {
        var sqlLobbyFilter = $"{MAP_PROP_KEY} = Map3 AND {GOLD_PROP_KEY} BETWEEN 300 AND 1000";
        var opJoinRandomRoomParams = new OpJoinRandomRoomParams
        {
            SqlLobbyFilter = sqlLobbyFilter
        };

        _lbc.OpJoinRandomRoom(opJoinRandomRoomParams);
    }

    public void OnLeftLobby()
    {
        
    }

    public void OnRoomListUpdate(List<RoomInfo> roomList)
    {
       
    }

    public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        
    }
}
