using System;
using EWorldsCore.Base.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerContent : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private TextMeshProUGUI _wizardNameText, _wandNameText;
    [SerializeField] private Image _wizardIcon, _wandIcon;
    [SerializeField] private CanvasGroup _leftGroupCanvas, _rightGroupCanvas;

    #endregion

    #region Unity LifeCycle

    private void Awake() => _leftGroupCanvas.alpha = _rightGroupCanvas.alpha = 0f;

    #endregion

    #region Utility Methods

    public void UpdatePlayerContent(WizardSO wizard)
    {
        var name = wizard.wizardName == WizardDB.WizardName.Gozoso ? "?" : EnumUtils.GetEnumDescription(wizard.wizardName);
        _wizardIcon.sprite = wizard.wizardIcon;
        _wizardNameText.text = name;
    }

    public void UpdateWand(WandSO wand)
    {
        _wandNameText.text = EnumUtils.GetEnumDescription(wand.wandName);
        _wandIcon.sprite = wand.wandIcon;
    }

    public void OnWizardFixed() => _rightGroupCanvas.alpha = _leftGroupCanvas.alpha = 1;
    public void OnWizardUnfixed() => _rightGroupCanvas.alpha = _leftGroupCanvas.alpha = 0;
    
    public void OnWandFixed() => _rightGroupCanvas.alpha = _leftGroupCanvas.alpha = 0;
    public void OnWandUnfixed() => _rightGroupCanvas.alpha = _leftGroupCanvas.alpha = 1;

    #endregion
}
