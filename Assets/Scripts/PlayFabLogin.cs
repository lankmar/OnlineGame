using PlayFab;
using PlayFab.ClientModels;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayFabLogin : MonoBehaviour
{
    private const string AuthGuidKey = "auth_guid_key";

    public void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = "CD905";
        }

        var needCreation = PlayerPrefs.HasKey(AuthGuidKey);
        var id = PlayerPrefs.GetString(AuthGuidKey, Guid.NewGuid().ToString());

        var request = new LoginWithCustomIDRequest
        {
            CustomId = id,
            CreateAccount = !needCreation
        };
        PlayFabClientAPI.LoginWithCustomID(request, result =>
        {
            PlayerPrefs.SetString(AuthGuidKey, id);
            OnLoginSuccess(result);
        }, OnLoginFailure);
    }
    private void OnLoginSuccess(LoginResult result)
    {

        Debug.Log("Congratulations, you made successful API call!");
        SetUsetData(result.PlayFabId);
    }

    private void SetUsetData(string playFabId)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        { 
            Data = new System.Collections.Generic.Dictionary<string, string>
            {
                {"time_recive_deily_reward", DateTime.UtcNow.ToString()}
            }
        },
        result =>
        {
            Debug.Log("SetUserData");
            GetUserData(playFabId, "time_recive_deily_reward");
        },
        OnLoginFailure);
    }

    private void GetUserData(string playFabId, string keyData)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest
        {
            PlayFabId = playFabId
        }, result =>
        {
            if (result.Data.ContainsKey(keyData))
                Debug.Log($"{keyData}: {result.Data[keyData].Value}");
        },
        OnLoginFailure);
    }

    private void OnLoginFailure(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.LogError($"Something went wrong: {errorMessage}");
    }

    #region old

    //private void Update()
    //{
    //    UpdateLabel(PlayFabClientAPI.IsClientLoggedIn());
    //}


    //public void UpdateLabel(bool isConnect)
    //{
    //    Debug.Log("PlayFabClientAPI.IsClientLoggedIn()" + PlayFabClientAPI.IsClientLoggedIn());
    //    if (isConnect)
    //    {
    //        _text.text = "Connect is true";
    //        _button.GetComponent<Graphic>().color = Color.green;
    //    }
    //    else
    //    {
    //        _text.text = "Connect is false";
    //        _button.GetComponent<Graphic>().color = Color.red;
    //    }
    //}
    #endregion
}
