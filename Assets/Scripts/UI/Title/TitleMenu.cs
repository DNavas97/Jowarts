using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    #region Private Variables
    
    #endregion

    #region Unity LifeCycle

    private void Update() => HandleInputs();

    #endregion

    #region Utility Methods

    private void HandleInputs()
    {
        if(Input.GetButtonDown("JumpP1") || Input.GetButtonDown("JumpP2")) LoadMainMenu();
    }

    private void LoadMainMenu() => SceneManager.LoadScene("PS_Lobby", LoadSceneMode.Single);

    #endregion
}
