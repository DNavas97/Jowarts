using UnityEngine;
using UnityEngine.Events;

public class TitleAnimation : MonoBehaviour
{
    #region Events

    public static UnityEvent onAnimationEnds = new UnityEvent();
    
    #endregion

    #region Utility Methods

    public void OnAnimationEnds() => onAnimationEnds?.Invoke();

    #endregion
}
