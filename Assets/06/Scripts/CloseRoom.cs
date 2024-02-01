using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using System;

public class CloseRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] Button _closeRoom;
    [SerializeField] TMP_Text _roomName;
    void Start()
    {
        _closeRoom.onClick.AddListener(CloseCurrentRoom);
        _roomName.text = PhotonNetwork.CurrentRoom.Name;
    }

    private void CloseCurrentRoom()
    {
        if (PhotonNetwork.CurrentRoom.IsOpen)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
        }
    }
}
