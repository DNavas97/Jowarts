using UnityEngine;

public class AnimationController : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private Animator _animator;
    
    private static readonly int JumpTrigger = Animator.StringToHash("Jump");
    private static readonly int AttackTrigger = Animator.StringToHash("Attack");
    private static readonly int HorizontalInput = Animator.StringToHash("HorizontalInput");
    
    #endregion

    #region Unity LifeCycle



    #endregion

    #region Utility Methods

    public void OnJump() => _animator.SetTrigger(JumpTrigger);
    public void OnAttack() => _animator.SetTrigger(AttackTrigger);

    public void UpdateHorizontalInput(float horizontalInput) => _animator.SetFloat(HorizontalInput, horizontalInput);

    #endregion


}