using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

public class SceneController : MonoBehaviour
{
    // OnClick Event Handlers
    public void StartOnClick()
    {
        SceneManager.LoadSceneAsync(SceneNames.GAME_LEVEL);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync(SceneNames.MAIN_MENU);
    }

    public void OptionsOnClick()
    {
        SceneManager.LoadSceneAsync(SceneNames.OPTIONS_MENU, LoadSceneMode.Additive);
    }

    public void OptionsBackOnClicked()
    {
        SceneManager.UnloadSceneAsync(SceneNames.OPTIONS_MENU);
    }
}
