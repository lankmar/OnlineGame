using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] Button _logOut;
    string gameVersion = "1";

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

        if (PhotonNetwork.IsConnected)
        {

            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.GameVersion = gameVersion;
        }
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN");
    }

    private void Update()
    {
        if (PhotonNetwork.IsConnected)
        {
        Debug.Log("Is Connected");

        }
        else
        {
        Debug.Log("Not Connected");

        }
    }

    public void LogOut()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
    }

    public override void OnDisable()
    {
        Debug.Log("OnDisable");
    }

}
