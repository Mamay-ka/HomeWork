using PlayFab;
using PlayFab.ClientModels;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayFabLogin : MonoBehaviour
{
    [SerializeField] private TMP_Text successText;
    [SerializeField] private GameObject createAccauntPanel;
    [SerializeField] private GameObject signIn;
    [SerializeField] private GameObject cancelButton;
    [SerializeField] private GameObject OkButton;

    [SerializeField] private TMP_Text _createErrorLabel;
    [SerializeField] private TMP_Text _signInErrorLabel;
    [SerializeField] private TMP_Text _loading;

    private string _username;
    private string _mail;
    private string _pass;

    private const string AuthGuidKey = "authorization-guid";


    public void Start()
    {
         //Here we need to check whether TitleId property is configured in settings or not
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            /*
            * If not we need to assign it to the appropriate variable manually
            * Otherwise we can just remove this if statement at all
            */
            PlayFabSettings.staticSettings.TitleId = " A823B";
         }
        var needCreation = PlayerPrefs.HasKey(AuthGuidKey);
        var id = PlayerPrefs.GetString(AuthGuidKey, Guid.NewGuid().ToString());
        PlayFabClientAPI.LoginWithCustomID(new LoginWithCustomIDRequest()
        {
            CustomId = id,
            CreateAccount = !needCreation
        }, success => { PlayerPrefs.SetString(AuthGuidKey, id); }, OnFailure);
    }
    private void OnLoginSuccess(LoginResult result)
    {
        successText.text = "Congratulations, you made successful API call!";
        successText.color = Color.green;
        Debug.Log("Congratulations, you made successful API call!");
    }
    private void OnLoginFailure(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        successText.text = errorMessage;
        successText.color = Color.red;
        Debug.LogError($"Something went wrong: {errorMessage}");
    }
    public void UpdateUsername(string username)
    {
        _username = username;
    }
    public void UpdateEmail(string mail)
    {
        _mail = mail;
    }
    public void UpdatePassword(string pass)
    {
        _pass = pass;
    }

    public void CreateAccount()
    {
        _loading.text = "Loading";//!!!

        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
        {
            Username = _username,
            Email = _mail,
            Password = _pass,
            RequireBothUsernameAndEmail = true,
           
        }, result =>
        {
            EnterLobby();
            Debug.Log($"Success: {_username}");
            
        }, error =>
        {
            Debug.LogError($"Fail: {error.ErrorMessage}");
        });

        
    }

    public void IsActive()
    {
        createAccauntPanel.SetActive( false );
        cancelButton.SetActive( false );
        OkButton.SetActive( false );
    }

    public void IsSignInActive()
    {
        signIn.SetActive( false );
        cancelButton.SetActive ( false );
        OkButton.SetActive( false );
        _loading.text = " ";
    }

    public void SetActive()
    {
        createAccauntPanel.SetActive(true);
        cancelButton.SetActive( true );
        OkButton.SetActive( true );
    }

    public void SetActiveSignIn()
    {
        signIn.SetActive(true);
        cancelButton.SetActive(true);
        OkButton.SetActive (true);
    }

    public void SignIn()
    {
        PlayFabClientAPI.LoginWithPlayFab(new LoginWithPlayFabRequest
        {
            Username = _username,
            Password = _pass
        }, result =>
        {
            _loading.text = " ";//!!!
            EnterLobby();
            Debug.Log($"Success: {_username}");
        }, error =>
        {
            _loading.text = "Error";//!!!
            Debug.LogError($"Fail: {error.ErrorMessage}");
        });

    }

    public void Back()
    {
        _createErrorLabel.text = "";
        _signInErrorLabel.text = "";
    }

    public void OnCreateSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log($"Creation Success: {_username}");
        
    }

    public void OnSignInSuccess(LoginResult result)
    {
        Debug.Log($"Sign In Success: {_username}");
        
    }

    public void OnFailure(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        Debug.LogError($"Something went wrong: {errorMessage}");
        _createErrorLabel.text = errorMessage;
        _signInErrorLabel.text = errorMessage;
    }

    private void EnterLobby()
    {
        SceneManager.LoadScene(1);
    }
}