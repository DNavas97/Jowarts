using System;
using System.Collections;
using EWorldsCore.Base.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerContent : MonoBehaviour
{
    #region Private Variables
    
    [SerializeField] private TextMeshProUGUI _wizardNameText, _wandNameText, _wandInfoNameText, _wandInfoText, wizardInfoText, wizardInfoNameText;
    [SerializeField] private Image _wizardIcon, _wandIcon;
    [SerializeField] private CanvasGroup _leftGroupCanvas, _rightGroupCanvas, _canvasInfo;
    
    private AudioSource _audioSource;
    private Coroutine _infoCoroutine;
    private bool _infoShowed;
    private WizardSO _wizard;

    #endregion

    #region Unity LifeCycle

    private void Awake()
    {
        TryGetComponent(out _audioSource);
        _leftGroupCanvas.alpha = _rightGroupCanvas.alpha = 0f;
    }

    #endregion

    #region Utility Methods

    public void UpdatePlayerContent(WizardSO wizard)
    {
        _wizard = wizard;
        
        var name = wizard.wizardName == WizardDB.WizardName.Gozoso ? "?" : EnumUtils.GetEnumDescription(wizard.wizardName);
        _wizardIcon.sprite = wizard.wizardIcon;
        _wizardNameText.text = name;
        
        wizardInfoNameText.text = name;
        wizardInfoText.text = wizard.description;
    }

    public void UpdateWand(WandSO wand)
    {
        _wandNameText.text = EnumUtils.GetEnumDescription(wand.wandName);
        _wandIcon.sprite = wand.wandIcon;
        
        _wandInfoNameText.text = EnumUtils.GetEnumDescription(wand.wandName);
        _wandInfoText.text = wand.description;
    }

    public void OnWizardFixed()
    {
        _rightGroupCanvas.alpha = _leftGroupCanvas.alpha = 1;
        PlayIntroductionSFX(_wizard.introductionSFX);
    }

    public void OnWizardUnfixed() => _rightGroupCanvas.alpha = _leftGroupCanvas.alpha = 0;

    public void OnWandFixed() => _rightGroupCanvas.alpha = _leftGroupCanvas.alpha = 0;
    public void OnWandUnfixed() => _rightGroupCanvas.alpha = _leftGroupCanvas.alpha = 1;

    public void OnShowInfoClicked()
    {
        if(_infoCoroutine != null) StopCoroutine(ShowInfo());
        _infoCoroutine = StartCoroutine(ShowInfo());
    }
    
    private IEnumerator ShowInfo()
    {
        _infoShowed = !_infoShowed;
        var t = 0f;
        var time = 0.2f;

        while (t < 1)
        {
            t += Time.deltaTime / time;
            var alpha = _infoShowed ? t : 1 - t;
            
            _canvasInfo.alpha = alpha;
            
            yield return null;
        }
    }

    private void PlayIntroductionSFX(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }

    #endregion
}
