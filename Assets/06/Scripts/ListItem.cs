using Photon.Realtime;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class ListItem : MonoBehaviour
{
    [SerializeField] TMP_Text _textName;
    [SerializeField] TMP_Text _textPlayerCount;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(JoinToListRoom);
    }

    public void SetInfo(RoomInfo info)
    {
        _textName.text = info.Name;
        _textPlayerCount.text = $"{info.PlayerCount}/{info.MaxPlayers}";
    }

    public void JoinToListRoom()
    {
        PhotonNetwork.JoinRoom(_textName.name);
    }
}
