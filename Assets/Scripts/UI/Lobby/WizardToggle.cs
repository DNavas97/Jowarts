using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WizardToggle : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private Image _toggleBorder, wizardIcon;
    [SerializeField] private Toggle _toggle;

    private WizardSO _wizard;
    
    #endregion

    #region Events

    public static UnityEvent<WizardSO> OnWizardSelected = new UnityEvent<WizardSO>();

    #endregion
    
    #region Unity LifeCycle

    #endregion

    #region Utility Methods

    public void Initialize(ToggleGroup tGroup, WizardSO wizardSo)
    {
        _wizard = wizardSo;
        
        _toggle.group = tGroup;
        wizardIcon.sprite = wizardSo.wizardIcon;
    }
    
    public void SuscribeToEvents() => _toggle.onValueChanged.AddListener(OnValueChanged);

    private void OnValueChanged(bool value)
    {
        _toggleBorder.color = value ? Color.green : Color.black;
        
        if(value) OnWizardSelected.Invoke(_wizard);
    }

    #endregion
}
