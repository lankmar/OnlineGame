using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun.Demo.PunBasics;
using Photon.Pun;

public class MyGameManager : MonoBehaviourPunCallbacks
{

	#region Public Fields

	static public MyGameManager Instance;

	#endregion

	#region Private Fields

	private GameObject instance;

	[Tooltip("The prefab to use for representing the player")]
	[SerializeField]
	private GameObject playerPrefab;

	#endregion

	#region MonoBehaviour CallBacks

	void Start()
	{
		Instance = this;

		// in case we started this demo with the wrong scene being active, simply load the menu scene
		if (!PhotonNetwork.IsConnected)
		{
			SceneManager.LoadScene("PhotonLobby07");

			return;
		}

		if (playerPrefab == null)
		{ // #Tip Never assume public properties of Components are filled up properly, always check and inform the developer of it.

			Debug.LogError("<Color=Red><b>Missing</b></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
		}
		else
		{


			if (PhotonNetwork.InRoom && PlayerManager.LocalPlayerInstance == null)
			{
				Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);

				// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
				PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
			}
			else
			{

				Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
			}


		}

	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			QuitApplication();
		}
	}

	#endregion

	#region Photon Callbacks

	public override void OnJoinedRoom()
	{
		if (PlayerManager.LocalPlayerInstance == null)
		{
			Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);

			// we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate
			//PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
		}
	}


	public override void OnPlayerEnteredRoom(Player other)
	{
		Debug.Log("OnPlayerEnteredRoom() " + other.NickName); // not seen if you're the player connecting

		if (PhotonNetwork.IsMasterClient)
		{
			Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

			LoadArena();
		}
	}

	public override void OnPlayerLeftRoom(Player other)
	{
		Debug.Log("OnPlayerLeftRoom() " + other.NickName); // seen when other disconnects

		if (PhotonNetwork.IsMasterClient)
		{
			Debug.LogFormat("OnPlayerEnteredRoom IsMasterClient {0}", PhotonNetwork.IsMasterClient); // called before OnPlayerLeftRoom

			LoadArena();
		}
	}

	public override void OnLeftRoom()
	{
		SceneManager.LoadScene("PhotonLobby07");
	}

	#endregion

	#region Public Methods

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
	}

	public void QuitApplication()
	{
		Application.Quit();
	}

	#endregion

	#region Private Methods

	void LoadArena()
	{
		if (!PhotonNetwork.IsMasterClient)
		{
			Debug.LogError("PhotonNetwork : Trying to Load a level but we are not the master Client");
			return;
		}

		Debug.LogFormat("PhotonNetwork : Loading Level Game07");

		PhotonNetwork.LoadLevel("Game07");
	}

	#endregion
}
