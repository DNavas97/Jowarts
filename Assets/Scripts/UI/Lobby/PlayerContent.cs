using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerContent : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private TextMeshProUGUI _wizardNameText;
    [SerializeField] private Image _wizardIcon, _wandIcon;

    #endregion

    #region Unity LifeCycle


    #endregion

    #region Utility Methods

    public void UpdatePlayerContent(WizardSO wizard)
    {
        _wizardIcon.sprite = wizard.wizardIcon;
        _wizardNameText.text = wizard.wizardName.ToString();
    }

    #endregion
}
