using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
public class PlayFabLogin : MonoBehaviour
{
    [SerializeField] private TMP_Text successText;

    public void Start()
    {
        // Here we need to check whether TitleId property is configured in settings or not
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            /*
            * If not we need to assign it to the appropriate variable manually
            * Otherwise we can just remove this if statement at all
            */
            PlayFabSettings.staticSettings.TitleId = " A823B";
        }
        var request = new LoginWithCustomIDRequest
        {
            CustomId = "GeekBrainsLesson3",
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
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
}