using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Private Variables

    private FightGameController _fightGameController;
    private WizardSO _wizardSo;
    private WandSO _wandSo;
    
    [SerializeField] private PlayerID _playerID;
    [SerializeField] private MovementController _movementController;
    [SerializeField] private MagicSpawner _magicSpawner;

    [NonSerialized] public int Health = 100;
    
    public enum PlayerID
    {
        None    = -1,
        Player1 = 0,
        Player2 = 1
    }
    
    #endregion

    #region Unity LifeCycle
    
    #endregion

    #region Utility Methods

    public void Initialize(FightGameController gc, WizardSO wizard, WandSO wand)
    {
        _fightGameController = gc;
        _wizardSo = wizard;
        _wandSo = wand;
        
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
            _fightGameController.OnGameEnd(this);
        }
        
        _fightGameController.OnPlayerTakeHit(this);
    }
    
    #endregion


}