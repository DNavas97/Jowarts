using System;
using UnityEngine;

public class WizardPreview : MonoBehaviour
{
    #region Private Variables

    private Animator _animator;
    
    private static readonly int ReadyTrigger = Animator.StringToHash("Ready");
    
    #endregion

    #region Unity LifeCycle

    private void Awake() => TryGetComponent(out _animator);

    #endregion

    #region Utility Methods

    public void OnPlayerReady() => _animator.SetTrigger(ReadyTrigger);

    #endregion
}
