using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;

public class PlayFabAccountManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _textLabel;
    [SerializeField] private TMP_Text _dopInformation;

    void Start()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetAccount, OnError);    
    }

    private void OnGetAccount(GetAccountInfoResult result)
    {
        _textLabel.text = $"Player id: {result.AccountInfo.PlayFabId}";
        _dopInformation.text = $"Username: {result.AccountInfo.Username},  Created: {result.AccountInfo.Created}";
    }

    private void OnError(PlayFabError error)
    {
        var errorMassage = error.GenerateErrorReport();
        Debug.Log(errorMassage);
    }

}
