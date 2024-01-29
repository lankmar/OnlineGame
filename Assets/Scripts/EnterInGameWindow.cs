using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterInGameWindow : MonoBehaviour
{
    [SerializeField] private Button _singInButton;
    [SerializeField] private Button _createAccountButton;
    [SerializeField] private Button _backSingInAccountButton;
    [SerializeField] private Button _backCreateAccountButton;

    [SerializeField] private Canvas _enterInGameCanvas;
    [SerializeField] private Canvas _singInCanvas;
    [SerializeField] private Canvas _createAccountCanvas;
    
    void Start()
    {
        _singInButton.onClick.AddListener(OpenSingInWindow);
        _createAccountButton.onClick.AddListener(OpenCreateAccountWindow);
        _backCreateAccountButton.onClick.AddListener(BackToExitWindow);
        _backSingInAccountButton.onClick.AddListener(BackToExitWindow);
    }

    private void BackToExitWindow()
    {
        _enterInGameCanvas.enabled = true;
        _singInCanvas.enabled = false;
        _createAccountCanvas.enabled = false;
    }

    private void OpenSingInWindow()
    {
        _singInCanvas.enabled = true;
        _enterInGameCanvas.enabled = false;
    }

    private void OpenCreateAccountWindow()
    {
        _createAccountCanvas.enabled = true;
        _enterInGameCanvas.enabled = false;
    }
}
