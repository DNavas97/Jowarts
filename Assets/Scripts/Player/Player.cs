﻿using System;
using System.Collections;
using Persistent_Data;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Public Variables

    public PlayerID PlayerId { get; private set; }

    #endregion
    
    #region Private Variables

    private FightGameController _fightGameController;
    private WizardSO _wizardSo;
    private WandSO _wandSo;
    
    [SerializeField] private MovementController _movementController;
    [SerializeField] private MagicSpawner _magicSpawner;
    
    private AnimationController _animationController;
    
    [NonSerialized] public int CurrentHealth;
    [NonSerialized] public int MaxHealth;
    
    private float _fireCooldown, _shieldCooldown, _playerSpeed, _jumpForce, _projectileSpeed,
                  _projectileSize, _shieldHeal, _projectileHeal, _poisonDuration;
    
    private int _projectileDamage, _resurrections, _poisonDamage, _instaKillProb;
    
    private bool  _canReflect;

    private Coroutine _poisonCoroutine;
    
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

    public void Initialize(PlayerID id, FightGameController gc, WizardSO wizard, WandSO wand)
    {
        PlayerId = id;
        TryGetComponent(out _animationController);
        
        _fightGameController = gc;
        _wizardSo = wizard;
        _wandSo = wand;
        
        SetInitialParams();
        SetSynergies();
        
        _movementController.Initialize(this);
        _magicSpawner.SetPlayer(this);
        
        _magicSpawner.OnFire.AddListener(TriggerAttackAnimation);

        ApplySynergies();
    }

    private void SetInitialParams()
    {
        _fireCooldown     = GlobalParams.BaseFireCooldown;
        _shieldCooldown   = GlobalParams.BaseShieldCooldown;
        _playerSpeed      = GlobalParams.BasePlayerSpeed;
        _jumpForce        = GlobalParams.BasePlayerJump;
        _projectileDamage = GlobalParams.BaseProjectileDamage;
        _projectileSpeed  = GlobalParams.BaseProjectileSpeed;
        _instaKillProb    = GlobalParams.BaseInstaKillProb;
        _resurrections    = GlobalParams.BaseResurrections;
        _projectileSize   = GlobalParams.BaseProjectileSize;
        _projectileHeal   = GlobalParams.BaseProjectileHeal;
        _canReflect       = GlobalParams.BaseCanReflect;
        _poisonDamage     = GlobalParams.BasePoisonDamage;
        _poisonDuration   = GlobalParams.PoisonDuration;
        _shieldHeal       = GlobalParams.BaseShieldHeal;
        
        CurrentHealth     = MaxHealth = GlobalParams.BaseHealth;
    }

    private void SetSynergies()
    {
        SetWizardSynergy();
        SetWandSynergy();
    }

    public void Fire() => _magicSpawner.Fire();

    public void Shield() => _magicSpawner.Shield(transform);

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;

            if (_resurrections > 0)
            {
                _resurrections--;
                Heal(MaxHealth/2);
            }
            else
            {
                _fightGameController.OnGameEnd(this);    
            }
        }
        
        _fightGameController.OnPlayerHealthUpdated(this);
    }

    public WizardSO GetWizard() => _wizardSo;

    private void SetWizardSynergy()
    {
        switch (_wizardSo.wizardName)
        {
            case WizardDB.WizardName.Hagrid:
                SetHealthMultiplier(GlobalParams.HagridHealthMultiplier);
                break;
            case WizardDB.WizardName.Harry:
                _resurrections += GlobalParams.HarryResurrections;
                break;
            case WizardDB.WizardName.Ron:
                _playerSpeed += _playerSpeed * GlobalParams.RonSpeedMultiplier;
                break;
            case WizardDB.WizardName.Hermione:
                _projectileSize += _projectileSize * GlobalParams.HermioneProjectileSizeMultiplier;
                break;
            case WizardDB.WizardName.Draco:
                _jumpForce += _jumpForce * GlobalParams.DracoJumpMultiplier;
                break;
            case WizardDB.WizardName.Snape:
                _shieldCooldown -= _shieldCooldown * GlobalParams.SnapeShieldCooldownMultiplier;
                break;
            case WizardDB.WizardName.Gozoso:
                _shieldHeal = GlobalParams.JovaniShieldHeal;
                break;
            case WizardDB.WizardName.Voldemort:
                _projectileDamage += (int)(_projectileDamage * GlobalParams.VoldemortDamageMultiplier);
                break;
            default:
                Debug.LogError("No existe el wizard");
                break;
        }
    }

    private void SetWandSynergy()
    {
        switch (_wandSo.wandName)
        {
            case WandDB.WandName.Speed:
                _projectileSpeed += _projectileSpeed * GlobalParams.WandProjectileSpeedMultiplier;
                break;
            case WandDB.WandName.FireCooldown:
                _fireCooldown -= _fireCooldown * GlobalParams.WandProjectileCooldownMultiplier;
                break;
            case WandDB.WandName.ShieldCooldown:
                _shieldCooldown -= _shieldCooldown * GlobalParams.WandShieldCooldownMultiplier;
                break;
            case WandDB.WandName.InstaKill:
                _instaKillProb += GlobalParams.WandInstaKillProbMultiplier;
                break;
            case WandDB.WandName.Heal:
                _projectileHeal = GlobalParams.WandProjectileHealMultiplier;
                break;
            case WandDB.WandName.Poison:
                _poisonDamage += GlobalParams.WandPoisonDamage;
                break;
            case WandDB.WandName.Reflect:
                _canReflect = GlobalParams.WandCanReflect;
                break;
            default:
                Debug.LogError("No existe la varita");
                break;
        }
    }

    private void SetHealthMultiplier(float multiplier)
    {
        MaxHealth += (int)(MaxHealth * multiplier);
        CurrentHealth = MaxHealth;
    }

    private void ApplySynergies()
    {
        _movementController.SetSpeed(_playerSpeed);
        _movementController.SetJumpForce(_jumpForce);
        
        _magicSpawner.SetDamage(_projectileDamage);
        _magicSpawner.SetProjectileSpeed(_projectileSpeed);
        _magicSpawner.SetFireCooldown(_fireCooldown);
        _magicSpawner.SetShieldCooldown(_shieldCooldown);
        _magicSpawner.CanReflect(_canReflect);
        _magicSpawner.SetInstaKillProb(_instaKillProb);
        _magicSpawner.SetPoisonDamage(_poisonDamage);
        _magicSpawner.SetProjectileSize(_projectileSize);
        _magicSpawner.SetProjectileHealMultiplier(_projectileHeal);
        _magicSpawner.SetShieldHealMultiplier(_shieldHeal);
    }

    public void Heal(int amount)
    {
        var newHealth = CurrentHealth + amount;
        CurrentHealth = newHealth > MaxHealth ? MaxHealth : newHealth;
        _fightGameController.OnPlayerHealthUpdated(this);
    }


    public void GetPoisoned(int damage)
    {
        if(_poisonCoroutine != null) StopCoroutine(_poisonCoroutine);
        _poisonCoroutine = StartCoroutine(OnPoison(damage));
    }

    private IEnumerator OnPoison(int damage)
    {
        var duration = GlobalParams.PoisonDuration;
        var damagePerSecond = (int)(damage / duration);

        while (duration > 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            TakeDamage(damagePerSecond);
            duration--;
        }
    }
    
    private void TriggerAttackAnimation() => _animationController.OnAttack();

    public void TriggerJumpAnimation() => _animationController.OnJump();

    public void UpdateHorizontalInput(float horizontalInput) => _animationController.UpdateHorizontalInput(horizontalInput);

    public void UpdateShieldCooldownView(float f) => _fightGameController.OnShieldCooldownUpdated(this, f);

    public void UpdateFireCooldownView(float f) =>_fightGameController.OnFireCooldownUpdated(this, f);
    
    #endregion
}