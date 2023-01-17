using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using PlayerID = Player.PlayerID;

public class WizardToggle : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private Image _toggleBorder, wizardIcon;
    [SerializeField] private Button _button;

    private WizardSO _wizard;
    private Player.PlayerID _selectedByPlayer;
    
    #endregion

    #region Events

    public static UnityEvent<WizardSO> OnWizardSelected = new UnityEvent<WizardSO>();
    public static UnityEvent<KeyValuePair<PlayerID, WizardToggle>> OnToggleSelected = new UnityEvent<KeyValuePair<PlayerID, WizardToggle>>();

    #endregion
    
    #region Unity LifeCycle

    #endregion

    #region Utility Methods

    public void Initialize(WizardSO wizardSo)
    {
        _wizard = wizardSo;
        
        wizardIcon.sprite = wizardSo.wizardIcon;
    }

    public void Select(PlayerID playerID)
    {
        _selectedByPlayer = playerID;
        OnButtonClicked();
    }

    public void SuscribeToEvents() => _button.onClick.AddListener(OnButtonClicked);

    public void OnValueChanged(bool value)
    {
        if(!value) _toggleBorder.color = Color.black;
        else       _toggleBorder.color = _selectedByPlayer == PlayerID.Player1 ? Color.red : Color.blue;
        
        if (value) OnWizardSelected.Invoke(_wizard);
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
