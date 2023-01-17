using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingPlayerInfo : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private Player.PlayerID _playerID;
    [SerializeField] private Image wizardImage;
    [SerializeField] private TextMeshProUGUI wizardNameText, wizardWandText;

    #endregion

    #region Unity LifeCycle

    private void Start() => Initialize();

    #endregion

    #region Utility Methods

    private void Initialize()
    {
        var persistenData = PersistentData.Instance;
        
        var wizard = _playerID == Player.PlayerID.Player1 ? persistenData.WizardP1 : persistenData.WizardP2;
        var wand = _playerID == Player.PlayerID.Player1 ? persistenData.WandP1 : persistenData.WandP2;

        wizardImage.sprite = wizard.wizardIcon;
        wizardNameText.text = wizard.wizardName.ToString();
        wizardWandText.text = wand.wandName.ToString();
    }
    
    #endregion
}
