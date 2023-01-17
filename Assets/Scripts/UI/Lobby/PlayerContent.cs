using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerContent : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private TextMeshProUGUI _wizardNameText;
    [SerializeField] private Image _wizardIcon, _wandIcon;
    [SerializeField] private Button _leftButton, _rightButton;

    private CanvasGroup _leftGroupCanvas, _rightGroupCanvas;
    private WandDB _wandDB;
    private int _currentPlayerSelection = 0;
    
    private int _currentWand = 0;
    
    #endregion

    #region Unity LifeCycle

    private void Awake()
    {
        _leftGroupCanvas = _leftButton.GetComponent<CanvasGroup>();
        _rightGroupCanvas = _rightButton.GetComponent<CanvasGroup>();

        _leftGroupCanvas.alpha = 0f;
        _rightGroupCanvas.alpha = 0f;

        _leftGroupCanvas.interactable = _rightGroupCanvas.interactable = false;
        
        _leftButton.onClick.AddListener(OnLeftButtonClicked);
        _rightButton.onClick.AddListener(OnRightButtonClicked);
    }

    private void Start() => _wandIcon.sprite = _wandDB.wands[0].wandIcon;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            OnConfirmCharacter();
    }

    #endregion

    #region Utility Methods

    public void Initialize(WandDB wandDB) => _wandDB = wandDB;

    public void UpdatePlayerContent(WizardSO wizard)
    {
        _wizardIcon.sprite = wizard.wizardIcon;
        _wizardNameText.text = wizard.wizardName.ToString();
    }
    
    private void OnRightButtonClicked()
    {
        _currentWand++;
        
        if(_currentWand > _wandDB.wands.Count) _currentWand = 0;

        _wandIcon.sprite = _wandDB.wands[_currentWand].wandIcon;
    }

    private void OnLeftButtonClicked()
    {
        _currentWand--;
        
        if (_currentWand < 0) _currentWand = _wandDB.wands.Count;

        _wandIcon.sprite = _wandDB.wands[_currentWand].wandIcon;
    }

    private void OnConfirmCharacter()
    {
        throw new NotImplementedException();
    }
    
    #endregion
}
