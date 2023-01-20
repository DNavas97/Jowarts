using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CountdownMenu : MonoBehaviour
{
    #region Private Variables
    
    [SerializeField] private AudioClip _readySFX, _fightSFX;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI _numberText;
    [SerializeField] private Image _gradientImage;

    private int _countDownMax = 3;
    private AudioSource _audioSource;

    #endregion
    
    #region Events

    public static UnityEvent OnGameStart = new UnityEvent();
    
    #endregion
    
    #region Unity LifeCycle

    #endregion

    #region Utility Methods

    public void Initialize() => StartCoroutine(ShowCoroutine());

    private IEnumerator ShowCoroutine()
    {
        GetComponent<AudioSource>().PlayOneShot(_readySFX);
        
        var t = 0f;
        var apppearTime = 0.5f;

        var roundNumber = PersistentData.Instance.RoundNumber + 1 == 3 ? "Final" : $"{PersistentData.Instance.RoundNumber + 1}";
        _numberText.text = $"Ronda {roundNumber}";
        
        while (t < 1)
        {
            t += Time.deltaTime / apppearTime;
            
            _canvasGroup.alpha = t;
            _numberText.transform.localScale = new Vector3(t, t, t);
            _gradientImage.transform.localScale = new Vector3(1, t, 1);

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1.5f);
        

        StartCoroutine(OnCountDownStart());
    }
    
    private IEnumerator OnCountDownStart()
    {
        _numberText.text = "Listos?";

        yield return new WaitForSecondsRealtime(1.5f);
        
        GetComponent<AudioSource>().PlayOneShot(_fightSFX);
        _numberText.text = "A peliar!";
        
        yield return new WaitForSecondsRealtime(1f);


        OnGameStart.Invoke();
        StartCoroutine(HideCoroutine());
    }

    private IEnumerator HideCoroutine()
    {
        var t = 0f;
        const float hideTime = 0.25f;
        
        while (t < 1)
        {
            t += Time.deltaTime / hideTime;

            var invertT = 1 - t;
            var gradientSizeY = 1 - t;
            var gradientSize = new Vector3(1, gradientSizeY, 1);
            _canvasGroup.alpha = 1 - t;
            _numberText.transform.localScale = new Vector3(invertT, invertT, invertT);
            _gradientImage.transform.localScale = gradientSize;

            yield return null;
        }
    }

    #endregion
}
