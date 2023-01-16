using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Private Variables

    private GameController _gameController;
    private CharacterSO _characterSo;
    private HouseSO _houseSo;
    
    [SerializeField] private PlayerID _playerID;
    [SerializeField] private MovementController _movementController;
    [SerializeField] private MagicSpawner _magicSpawner;

    [NonSerialized] public int Health = 100;
    
    public enum PlayerID
    {
        Player1 = 0,
        Player2 = 1
    }
    
    #endregion

    #region Unity LifeCycle
    
    #endregion

    #region Utility Methods

    public void Initialize(GameController gc)
    {
        _gameController = gc;
        
        _movementController.Initialize(this);
        _magicSpawner.Initialize(this); 
    }
    
    public PlayerID GetPlayerID() => _playerID;

    public void Fire() => _magicSpawner.Fire();

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            Health = 0;
            _gameController.OnGameEnd(this);
        }
        
        _gameController.OnPlayerTakeHit(this);
    }
    
    #endregion


}