using UnityEngine;

public class MovementController : MonoBehaviour
{
    #region Private Variables
    
    [SerializeField] private float _playerSpeed = 3.5f;
    [SerializeField] private float _jumpForce   = 5.0f;
    [SerializeField] private float _gravity     = -9.81f;

    private Vector3 _velocity;
    private CharacterController _characterController;
    private Player _player;
    private bool _gameEnded = false;

    private string _jumpButton, _horizontalButton, _fireButton;
    
    #endregion

    #region Unity LifeCycle

    private void Awake()
    {
        TryGetComponent(out _characterController);
        FightGameController.OnGameEnded.AddListener(OnGameEnd);
    }

    private void Update() => InputHandler();
    
    #endregion

    #region Utility Methods

    public void Initialize(Player player)
    {
        _player = player;
        var playerID = _player.GetPlayerID();
        
        _jumpButton = playerID == Player.PlayerID.Player1 ? "JumpP1" : "JumpP2";
        _horizontalButton = playerID == Player.PlayerID.Player1 ? "HorizontalP1" : "HorizontalP2";
        _fireButton = playerID == Player.PlayerID.Player1 ? "FireP1" : "FireP2";
    }
    
    private void InputHandler()
    {
        if(_gameEnded) return;
        
        JumpHandler();
        MovementHandler();
        FireHandler();
    }

    private void FireHandler()
    {
        if (Input.GetButtonDown(_fireButton)) _player.Fire();
    }

    private void JumpHandler()
    {
        if (_characterController.isGrounded)
        {
            _velocity.y = -1f;

            if (Input.GetButtonDown(_jumpButton))
                _velocity.y = _jumpForce;
        }
        else
        {
            _velocity.y -= _gravity * -2f * Time.deltaTime;
        }
    }

    private void MovementHandler()
    {
        var horizontalInput = new Vector3(Input.GetAxis(_horizontalButton), 0, 0);
        var movementVector = transform.TransformDirection(horizontalInput);
        var finalVector = _player.GetPlayerID() == Player.PlayerID.Player1 ? movementVector : -movementVector;

        _characterController.Move(finalVector * _playerSpeed * Time.deltaTime);
        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void OnGameEnd() => _gameEnded = true;

    #endregion
}