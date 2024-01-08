using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayFabLogin : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _text;

    bool _isConnect = false;

    public void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = " CD905";
        }
        var request = new LoginWithCustomIDRequest
        {
            CustomId = "Player1",
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }
    private void OnLoginSuccess(LoginResult result)
    {
        _isConnect = true;
        Debug.Log("Congratulations, you made successful API call!");
    }
    private void OnLoginFailure(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.LogError($"Something went wrong: {errorMessage}");
    }

    private void Update()
    {
        UpdateLabel(PlayFabClientAPI.IsClientLoggedIn());
    }


    public void UpdateLabel(bool isConnect)
    {
        Debug.Log("PlayFabClientAPI.IsClientLoggedIn()" + PlayFabClientAPI.IsClientLoggedIn());
        if (isConnect)
        {
            _text.text = "Connect is true";
            _button.GetComponent<Graphic>().color = Color.green;
        }
        else
        {
            _text.text = "Connect is false";
            _button.GetComponent<Graphic>().color = Color.red;
        }
    }
}
