using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Private Variables

    private UIController _uiController;
    
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

    private void Start()
    {
        _uiController = UIController.Instance;
        
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
        Health -= damage;

        if (Health <= 0)
        {
            Health = 0;
            _uiController.SetWinnerText(PlayerID.Player1.ToString());
        }
        
        _uiController.UpdatePlayerHealth(this);
    }
}