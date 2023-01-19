using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private CanvasGroup _tipCanvas;
    private bool _canInput;
    
    #endregion

    #region Unity LifeCycle

    private void Awake() => TitleAnimation.onAnimationEnds.AddListener(OnAnimationEnds);
    private void Update() => HandleInputs();

    #endregion

    #region Utility Methods

    private void HandleInputs()
    {
        if(!_canInput) return;
        
        if(Input.GetButtonDown("JumpP1") || Input.GetButtonDown("JumpP2")) LoadMainMenu();
    }

    private void LoadMainMenu() => SceneManager.LoadScene("PS_Lobby", LoadSceneMode.Single);

    private void OnAnimationEnds() => StartCoroutine(ShowTip());
    
    private IEnumerator ShowTip()
    {
        var t = 0f;
        var time = 0.2f;

        while (t < 1)
        {
            t += Time.deltaTime / time;
            
            _tipCanvas.alpha = t;
            
            yield return null;
        }
        _canInput = true;
    }
    
    #endregion
}
