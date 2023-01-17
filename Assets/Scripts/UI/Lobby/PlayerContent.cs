using System;
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
        _wizardIcon.sprite = wizard.wizardIcon;
        _wizardNameText.text = wizard.wizardName.ToString();
    }

    public void UpdateWand(WandSO wand)
    {
        _wandNameText.text = wand.wandName.ToString();
        _wandIcon.sprite = wand.wandIcon;
    }
    
    public void OnRightButtonClicked()
    {
        Debug.Log("Animar Right");
    }

    public void OnLeftButtonClicked()
    {
        Debug.Log("Animar Left");
    }

    public void OnWizardFixed() => _rightGroupCanvas.alpha = _leftGroupCanvas.alpha = 1;
    
    public void OnWandFixed() => _rightGroupCanvas.alpha = _leftGroupCanvas.alpha = 0;

    #endregion
}
