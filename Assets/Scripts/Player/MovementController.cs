using Persistent_Data;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    #region Private Variables
    
    [SerializeField] private float _gravity  = -9.81f;

    private float _playerSpeed;
    private float _jumpForce;

    private Vector3 _velocity;
    private CharacterController _characterController;
    private Player _player;
    private bool _movementEnabled;

    private KeyCode _jumpButton, _fireButton, _shieldButton;
    private string _horizontalButton;
    
    #endregion

    #region Unity LifeCycle

    private void Awake()
    {
        TryGetComponent(out _characterController);
        FightGameController.OnRoundEnded.AddListener(OnGameEnd);
    }

    private void Update() => InputHandler();
    
    #endregion

    #region Utility Methods

    public void Initialize(Player player)
    {
        CountdownMenu.OnGameStart.AddListener(OnGameStart);
        _player = player;

        var playerID = _player.PlayerId;

        _jumpButton = playerID == Player.PlayerID.Player1 ? GlobalParams.JumpInputP1 : GlobalParams.JumpInputP2;
        _horizontalButton = playerID == Player.PlayerID.Player1 ? "HorizontalP1" : "HorizontalP2";
        _fireButton = playerID == Player.PlayerID.Player1 ? GlobalParams.FireInputP1 : GlobalParams.FireInputP2;
        _shieldButton = playerID == Player.PlayerID.Player1 ? GlobalParams.ShieldInputP1 : GlobalParams.ShieldInputP2;
    }

    public void SetSpeed(float speed) => _playerSpeed += speed;
    public void SetJumpForce(float jumpForce) => _jumpForce = jumpForce;
    
    
    private void InputHandler()
    {
        if(!_movementEnabled) return;
        
        JumpHandler();
        MovementHandler();
        FireHandler();
        ShieldHandler();
    }

    private void FireHandler()
    {
        if (Input.GetKeyDown(_fireButton)) _player.Fire();
    }    
    
    private void ShieldHandler()
    {
        if (Input.GetKeyDown(_shieldButton)) _player.Shield();
    }

    private void JumpHandler()
    {
        if (_characterController.isGrounded)
        {
            _velocity.y = -1f;

            if (!Input.GetKeyDown(_jumpButton)) return;
            
            _velocity.y = _jumpForce;
            _player.TriggerJumpAnimation();
        }
        else
        {
            _velocity.y -= _gravity * -2f * Time.deltaTime;
        }
    }

    private void MovementHandler()
    {
        var horizontalInput = Input.GetAxis(_horizontalButton);
        if (horizontalInput != 0) horizontalInput = horizontalInput > 0 ? 1 : -1;
        
        _player.UpdateHorizontalInput(horizontalInput);
        
        var horizontalvector = new Vector3(horizontalInput, 0, 0);
        var movementVector = transform.TransformDirection(horizontalvector);
        var finalVector = _player.PlayerId == Player.PlayerID.Player1 ? movementVector : -movementVector;
        
        _characterController.Move(finalVector * _playerSpeed * Time.deltaTime);
        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void OnGameEnd() => _movementEnabled = false;
    private void OnGameStart() => _movementEnabled = true;

    #endregion
}