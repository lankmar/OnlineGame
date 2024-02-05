using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class PlayFabAccountManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _textLabel;
    [SerializeField] private TMP_Text _dopInformation;

    [SerializeField] private GameObject _newCharacterCreatePanel;
    [SerializeField] private Button _createCharButton;
    [SerializeField] private TMP_InputField _charNameInputField;
    [SerializeField] private List<MySlotCharWiget> _slots;

    private string _charName;


    void Start()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetAccount, OnError);
        PlayFabClientAPI.GetCatalogItems(new GetCatalogItemsRequest(), OnGetCatalogSuccess, OnError);
        PlayFabServerAPI.GetRandomResultTables(new PlayFab.ServerModels.GetRandomResultTablesRequest(), OnGetRandomResult, OnError);

        GetCharacters();

        foreach (var slot in _slots)
        {
            slot.SlotButton.onClick.AddListener(OpenCreateNewChar);
        }

        _charNameInputField.onValueChanged.AddListener(OnNameChanged);
        _createCharButton.onClick.AddListener(CreateChar);
    }

    private void CreateChar()
    {
        PlayFabClientAPI.GrantCharacterToUser(new GrantCharacterToUserRequest
        {
            CharacterName = _charName,
            ItemId = "character_token"
        }, result =>
        {
            Debug.Log("CreateChar");
            UpdateCharStatistic(result.CharacterId);
        },
        OnError
        );
        
    }

    private void UpdateCharStatistic(string characterId)
    {
        PlayFabClientAPI.UpdateCharacterStatistics( new UpdateCharacterStatisticsRequest
        { 
            CharacterId = characterId,
            CharacterStatistics = new Dictionary<string, int>
            {
                { "Damage", 10 },
                { "Hp", 100},
                { "Experience", 10}
            }
        }, result =>
        {
            Debug.Log("Complete");
            CloseCreateNewChar();
            GetCharacters();
        },
        OnError);
    }

    private void OnNameChanged(string name)
    {
        _charName = name;
    }

    private void OpenCreateNewChar()
    {
        _newCharacterCreatePanel.SetActive(true);
    }

    private void CloseCreateNewChar()
    {
        _newCharacterCreatePanel.SetActive(false);
    }

    private void GetCharacters()
    {
        PlayFabClientAPI.GetAllUsersCharacters(new ListUsersCharactersRequest(),
            result=>
            {
                Debug.Log($"Char count {result.Characters.Count}");
                ShowCharsSlot(result.Characters);
            }, OnError
            );
    }

    private void ShowCharsSlot(List<CharacterResult> characters)
    {
        if (characters.Count == 0)
        {
            foreach (var slot in _slots)
                slot.ShowEmptySlot();
        }
        else if (characters.Count > 0 && (characters.Count < _slots.Count))
        {
            PlayFabClientAPI.GetCharacterStatistics( new GetCharacterStatisticsRequest
            { 
                CharacterId = characters.First().CharacterId
            }, result =>
            {
                var damage = result.CharacterStatistics["Damage"].ToString();
                var hp = result.CharacterStatistics["Hp"].ToString();
                var exp = result.CharacterStatistics["Experience"].ToString();

                _slots.First().ShowInfoCharSlot(damage, hp, exp);
            }, OnError
            );
        }
        else
        {
            Debug.LogError($"add slot fo char");
        }
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
