using System;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    #region Private Variables
    
    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private float _jumpHeight = 1.0f;

    private Player _player;
    private const float GravityValue = -9.81f;
    private CharacterController _controller;
    private Vector3 _playerVelocity;
    private bool _groundedPlayer;

    private string _jumpButton, _horizontalButton, _fireButton;
    
    #endregion

    #region Unity LifeCycle

    private void Awake() => TryGetComponent(out _controller);



    private void Update()
    {
        InputHandler();
        FireHandler();
    }

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
        _groundedPlayer = _controller.isGrounded;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = 0f;
        }

        var move = new Vector3(Input.GetAxis(_horizontalButton), 0, 0);
        _controller.Move(move * (Time.deltaTime * _playerSpeed));

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown(_jumpButton) && _groundedPlayer)
        {
            _playerVelocity.y += Mathf.Sqrt(_jumpHeight * -3.0f * GravityValue);
        }

        _playerVelocity.y += GravityValue * Time.deltaTime;
        _controller.Move(_playerVelocity * Time.deltaTime);
    }

    private void FireHandler()
    {
        if (Input.GetButtonDown(_fireButton)) _player.Fire();
    }

    #endregion
}