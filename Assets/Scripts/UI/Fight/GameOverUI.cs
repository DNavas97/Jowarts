using System.Collections;
using Persistent_Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI _winnerText;
    [SerializeField] private Button _rematchButton, _menuButton;
    [SerializeField] private Transform buttonLayout;
    
    private Player.PlayerID _winner = Player.PlayerID.None;
    private bool _canInput;
    private int _selectedOption = 0;
    private int _unselectedOption = 1;

    private FightGameController _fightGameController;
    
    #endregion
    
    #region Unity LifeCycle
    
    private void Update()
    {
        if(_winner == Player.PlayerID.None) return;
        
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
        var vertical = _winner == Player.PlayerID.Player1 ? "VerticalP2" : "VerticalP1";
        var verticalInput = Input.GetAxis(vertical);

        if (verticalInput == 0 || !_canInput) return;
        
        _selectedOption = _selectedOption == 0 ? 1 : 0;
        _unselectedOption = _selectedOption == 0 ? 1 : 0;
        
        OnButtonSelected();
        
        StartCoroutine(InputDelay());
    }

    private void HandleConfirmationInput()
    {
        var confirmation = _winner == Player.PlayerID.Player1 ? GlobalParams.SubmitInputP2 : GlobalParams.SubmitInputP1;

        if (!Input.GetKeyDown(confirmation)) return;
        
        var button = buttonLayout.GetChild(_selectedOption).GetComponent<Button>();
        button.onClick.Invoke();
    }

    public void ShowWithWinner(Player player)
    {
        _winner = player.PlayerId;
        
        _canvasGroup.alpha = 1;
        _winnerText.text = "Gana " + player.GetWizard().wizardName;

        OnButtonSelected();
        
        _canInput = true;
    }

    private void OnRematchButonClicked()
    {
        var scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }

    private void OnMenuButonClicked() => SceneManager.LoadScene("PS_Lobby", LoadSceneMode.Single);

    private void OnButtonSelected()
    {
        var selectedImage = buttonLayout.GetChild(_selectedOption).GetComponent<Image>();
        var unselectedImage = buttonLayout.GetChild(_unselectedOption).GetComponent<Image>();
        
        selectedImage.color = Color.yellow;
        unselectedImage.color = Color.white;
    }
    
    private IEnumerator InputDelay()
    {
        _canInput = false;
            
        yield return new WaitForSecondsRealtime(0.2f);

        _canInput = true;
    }
    
    #endregion
}