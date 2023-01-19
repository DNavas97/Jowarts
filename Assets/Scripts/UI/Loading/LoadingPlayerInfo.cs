using EWorldsCore.Base.Scripts.Utils;
using Persistent_Data;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LoadingPlayerInfo : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private Player.PlayerID id;
    [SerializeField] private Image wandImage;
    [SerializeField] private TextMeshProUGUI wizardNameText, wizardWandText;
    [SerializeField] private GameObject _readyText, _tipText;

    private bool _ready;
    
    #endregion

    #region Events

    public UnityEvent OnPlayerReady = new UnityEvent();

    #endregion

    #region Unity LifeCycle

    #endregion

    #region Utility Methods

    public void Initialize(WizardSO wizard, WandSO wand)
    {
        wandImage.sprite = wand.wandIcon;
        wizardNameText.text = EnumUtils.GetEnumDescription(wizard.wizardName);
        wizardWandText.text = EnumUtils.GetEnumDescription(wand.wandName);
    }

    private void Update()
    {
        var input = id == Player.PlayerID.Player1 ? GlobalParams.SubmitInputP1 : GlobalParams.SubmitInputP2;
        
        if(!Input.GetKeyDown(input) || _ready) return;
            
        _readyText.SetActive(true);
        _tipText.SetActive(false);

        _ready = true;
        OnPlayerReady?.Invoke();
    }

    #endregion
}
