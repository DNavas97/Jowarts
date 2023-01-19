using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CountdownMenu : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI _numberText;
    [SerializeField] private Image _gradientImage, _shadowImage;

    private int _countDownMax = 3;

    #endregion
    
    #region Events

    public static UnityEvent OnGameStart = new UnityEvent();
    
    #endregion
    
    #region Unity LifeCycle



    #endregion

    #region Utility Methods

    public void Initialize()
    {
        StartCoroutine(OnCountDownStart());
    }

    private IEnumerator OnCountDownStart()
    {
        _canvasGroup.alpha = 1;
        var count = _countDownMax;

        while (count > 0)
        {
            _numberText.text = count.ToString();
            count--;
            yield return new WaitForSecondsRealtime(1f);
        }

        _numberText.text = "Ustedes pelien!";
        
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
