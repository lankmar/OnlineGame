using System;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;


public class CreateAccountWindow : AccountDataWindowsBase
{
    [SerializeField] private InputField _mailField;
    [SerializeField] private Button _createAccountButton;

    protected string _mail;

    [SerializeField] public Canvas _loadingAccountCanvas;

    protected override void SubscriptionElementUi()
    {
        base.SubscriptionElementUi();
        _mailField.onValueChanged.AddListener(UpdateMail);
        _createAccountButton.onClick.AddListener(CreateAccount);
    }

    private void CreateAccount()
    {
        _loadingAccountCanvas.enabled = true;
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
        { 
            Username = _userName,
            Email = _mail,
            Password = _password
        }, result =>
        {
            Debug.Log($"Success: {_userName}");
            EnterInGameScene();
        }, error =>
        {
            Debug.Log($"Error: {error.ErrorMessage}");
        });
        _loadingAccountCanvas.enabled = false;
    }

    private void UpdateMail(string mail)
    {
        _mail = mail;
    }
  
}
