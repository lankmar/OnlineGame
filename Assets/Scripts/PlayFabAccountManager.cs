using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;

public class PlayFabAccountManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _textLabel;
    [SerializeField] private TMP_Text _dopInformation;

    void Start()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetAccount, OnError);    
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest(), OnGetCatalogSuccess, OnError);
        PlayFabServerAPI.GetRandomResultTables(new PlayFab.ServerModels.GetRandomResultTablesRequest(), OnGetRandomResult, OnError);
    }

    private void OnGetRandomResult(PlayFab.ServerModels.GetRandomResultTablesResult result)
    {
        if (result != null)
        {
            var count = result.Tables.Keys;

        }
    }

    private void OnGetAccount(GetAccountInfoResult result)
    {
        _textLabel.text = $"Player id: {result.AccountInfo.PlayFabId}";
        _dopInformation.text = $"Username: {result.AccountInfo.Username},  Created: {result.AccountInfo.Created}";
    }

    private void OnGetCatalogSuccess(GetCatalogItemsResult result)
    {
        Debug.Log("OnGetCatalogSuccess");
        ShoeItems(result.Catalog);
        //_textLabel.text = $"Player id: {result.AccountInfo.PlayFabId}";
        //_dopInformation.text = $"Username: {result.AccountInfo.Username},  Created: {result.AccountInfo.Created}";
    }

    private void ShoeItems(List<CatalogItem> catalog)
    {
        foreach (var item in catalog)
        {
            Debug.Log($"{item.DisplayName}");
        }
    }

    private void OnError(PlayFabError error)
    {
        var errorMassage = error.GenerateErrorReport();
        Debug.Log(errorMassage);
    }

}
