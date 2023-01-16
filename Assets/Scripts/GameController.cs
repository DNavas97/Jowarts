using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Public Variables

    public static GameController Instance;

    #endregion
    
    #region Private Variables

    [SerializeField] private Player _player1, _player2;
    [SerializeField] private UIController _uiController;

    #endregion

    #region Unity LifeCycle

    private void Awake()
    {
        if (Instance != null) Instance = this;
        else Destroy(gameObject);
        
        Initialize();
    }

    #endregion

    #region Utility Methods

    private void Initialize()
    {
        _player1.Initialize(this);
        _player2.Initialize(this);
    }
    
    public void OnPlayerTakeHit(Player player) => _uiController.UpdatePlayerHealth(player);

    public void OnGameEnd(Player loser)
    {
        var winner = loser == _player1 ? _player2 : _player1;
        
        _uiController.OnGameEnd(winner);
    }
    
    #endregion
}