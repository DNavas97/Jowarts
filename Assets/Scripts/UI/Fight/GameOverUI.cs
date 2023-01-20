using System;
using System.Collections;
using EWorldsCore.Base.Scripts.Utils;
using Persistent_Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _gryffindor;
    
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI _winnerText;
    [SerializeField] private Button _rematchButton, _menuButton;
    [SerializeField] private Transform buttonLayout;
    [SerializeField] private Color32 yellow;
    
    private Player.PlayerID _loser = Player.PlayerID.None;
    private bool _canInput;
    private int _selectedOption = 0;
    private int _unselectedOption = 1;

    private FightGameController _fightGameController;
    
    #endregion
    
    #region Unity LifeCycle
    
    private void Update()
    {
        if(_loser == Player.PlayerID.None) return;
        
        HandleInputs();
    }

    private void Start()
    {
        _rematchButton.onClick.AddListener(OnRematchButonClicked);
        _menuButton.onClick.AddListener(OnMenuButonClicked);
    }

    #endregion

    #region Utility Methods

    private void HandleInputs()
    {
        HandleSelectionInput();
        HandleConfirmationInput();
    }
    
    private void HandleSelectionInput()
    {
        var horizontal = _loser == Player.PlayerID.Player1 ? "HorizontalP1" : "HorizontalP2";
        var horizontalInput = Input.GetAxis(horizontal);

        if (horizontalInput == 0 || !_canInput) return;
        
        _selectedOption = _selectedOption == 0 ? 1 : 0;
        _unselectedOption = _selectedOption == 0 ? 1 : 0;
        
        OnButtonSelected();
        
        StartCoroutine(InputDelay());
    }

    private void HandleConfirmationInput()
    {
        var confirmation = _loser == Player.PlayerID.Player1 ? GlobalParams.SubmitInputP1 : GlobalParams.SubmitInputP2;

        if (!Input.GetKeyDown(confirmation)) return;
        
        var button = buttonLayout.GetChild(_selectedOption).GetComponentInChildren<Button>();
        button.onClick.Invoke();
    }

    public void ShowWithLoser(Player player)
    {
        _loser = player.PlayerId;

        StartCoroutine(ShowMenu());

        _winnerText.text = $"(HA PERDIDO {EnumUtils.GetEnumDescription(player.GetWizard().wizardName)})";

        OnButtonSelected();
    }

    private void OnRematchButonClicked()
    {
        PersistentData.Instance.ResetCounter();
        
        var scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    private void OnMenuButonClicked() => SceneManager.LoadScene("PS_Lobby", LoadSceneMode.Single);

    private void OnButtonSelected()
    {
        var selectedImage = buttonLayout.GetChild(_selectedOption).GetChild(0).GetComponent<Image>();
        var unselectedImage = buttonLayout.GetChild(_unselectedOption).GetChild(0).GetComponent<Image>();
        
        selectedImage.color = yellow;
        unselectedImage.color = Color.white;
    }
    
    private IEnumerator InputDelay()
    {
        _canInput = false;
            
        yield return new WaitForSecondsRealtime(0.2f);

        _canInput = true;
    }

    private IEnumerator ShowMenu()
    {
        var t = 0f;
        var appearTime = 0.3f;

        while (t < 1)
        {
            t += Time.deltaTime / appearTime;
            
            _canvasGroup.alpha = t;
            yield return null;
        }

        StartCoroutine(WaitForClipEnd());
        _audioSource.PlayOneShot(_gryffindor);

        yield return new WaitForSecondsRealtime(2f);

        _canInput = true;
    }

    private IEnumerator WaitForClipEnd()
    {        
        PersistentData.Instance.SetMusicVolume(0.2f);
        yield return new WaitForSeconds(_gryffindor.length);
        PersistentData.Instance.SetMusicVolume(1);
    }
    
    #endregion
}