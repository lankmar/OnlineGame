using System;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;

public class SingInWindow : AccountDataWindowsBase
{
    [SerializeField] private Button _singInButton;
    [SerializeField] private Button _backButton;

    [SerializeField] public Canvas _loadingAccountCanvas;

    protected override void SubscriptionElementUi()
    {
        base.SubscriptionElementUi();
        _singInButton.onClick.AddListener(ShowLoading);
        _singInButton.onClick.AddListener(SingIn);
    }

    private void SingIn()
    {
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
        { 
            Username = _userName,
            Password = _password
        }, result =>
        {
            Debug.Log($"Success: {_userName}");
            ShowLoading();
            EnterInGameScene();
        }, error =>
        {
            ShowLoading();
            Debug.Log($"Error: {error.ErrorMessage}");
        });
    }

    private void ShowLoading()
    {
        _loadingAccountCanvas.enabled = !_loadingAccountCanvas.isActiveAndEnabled;
    }
}
