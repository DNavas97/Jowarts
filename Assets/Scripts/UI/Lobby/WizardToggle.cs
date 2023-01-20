using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using PlayerID = Player.PlayerID;

public class WizardToggle : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private Sprite _mrXSprite;
    [SerializeField] private Image _toggleBorder, wizardIcon;
    [SerializeField] private Button _button;
    [SerializeField] private Color32 _redColor, _blueColor, _blackColor;

    private WizardSO _wizard;
    private PlayerID _selectedByPlayer;
    
    #endregion

    #region Events

    public static UnityEvent<KeyValuePair<PlayerID, WizardSO>> OnWizardSelected = new UnityEvent<KeyValuePair<PlayerID, WizardSO>>();
    public static UnityEvent<KeyValuePair<PlayerID, WizardToggle>> OnToggleSelected = new UnityEvent<KeyValuePair<PlayerID, WizardToggle>>();

    #endregion
    
    #region Unity LifeCycle

    #endregion

    #region Utility Methods

    public void Initialize(WizardSO wizardSo)
    {
        _wizard = wizardSo;

        wizardIcon.sprite = wizardSo.wizardName == WizardDB.WizardName.Gozoso ? _mrXSprite : wizardSo.wizardIcon;
    }

    public void Select(PlayerID playerID)
    {
        _selectedByPlayer = playerID;
        OnButtonClicked();
    }

    public void SuscribeToEvents() => _button.onClick.AddListener(OnButtonClicked);

    public void OnValueChanged(bool value)
    {
        if(!value) _toggleBorder.color = _blackColor;
        else       _toggleBorder.color = _selectedByPlayer == PlayerID.Player1 ? _redColor : _blueColor;

        if (value)
        {
            var keyValuePair = new KeyValuePair<PlayerID, WizardSO>(_selectedByPlayer, _wizard);
            OnWizardSelected.Invoke(keyValuePair);
        }
        else _selectedByPlayer = PlayerID.None;
    }

    private void OnButtonClicked()
    {
        var keyValuePair = new KeyValuePair<PlayerID, WizardToggle>(_selectedByPlayer, this);
        
        OnToggleSelected.Invoke(keyValuePair);
    }

    public bool IsSelected() => _selectedByPlayer != PlayerID.None;

    #endregion
}
