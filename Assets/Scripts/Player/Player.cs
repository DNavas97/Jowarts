using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Private Variables
        
    [SerializeField] private PlayerID _playerID;
    [SerializeField] private MovementController _movementController;
    [SerializeField] private MagicSpawner _magicSpawner;
    [SerializeField] private HealthBar _healthBar;

    private int _health = 100;
    
    public enum PlayerID
    {
        Player1 = 0,
        Player2 = 1
    }
    
    #endregion

    #region Unity LifeCycle

    private void Start()
    {
        _movementController.Initialize(this);
        _magicSpawner.Initialize(this);
    }

    #endregion

    #region Utility Methods

    public PlayerID GetPlayerID() => _playerID;

    #endregion

    public void Fire()
    {
        _magicSpawner.Fire();
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        
        _healthBar.UpdateHealthBar(_health);
    }
}