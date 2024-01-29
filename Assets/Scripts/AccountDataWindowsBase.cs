using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccountDataWindowsBase : MonoBehaviour
{
    [SerializeField] private InputField _userNameField;
    [SerializeField] private InputField _passwordField;

    protected string _userName;
    protected string _password;

    private void Start()
    {
        SubscriptionElementUi();
    }

    protected virtual void SubscriptionElementUi()
    {
        _userNameField.onValueChanged.AddListener(UpdateUserName);
        _passwordField.onValueChanged.AddListener(UpdateUserPassword);
    }

    private void UpdateUserName(string userName)
    {
        _userName = userName;
    }
    private void UpdateUserPassword(string password)
    {
        _password = password;
    }

    protected void EnterInGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
