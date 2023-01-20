using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class MagicSpawner : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private AudioClip _fireSFX, _shieldSFX;
    [SerializeField] private GameObject _magicProjectilePrefab;
    [SerializeField] private GameObject _redShield, _blueShield;

    private AudioSource _audioSource;
    
    private GameObject _shieldPrefab;

    private Player _player;
    private int   _projectileDamage;
    private float _projectileSpeed;
    private float _projectileCooldown;
    private float _shieldCooldown;
    private int _instaKillProbability;
    private bool  _canReflect;
    private int   _poisonDamage;
    private float _sizeMultiplier;
    private float _projectileHealMultiplier;
    private float _shieldHealMultiplier;
    private GameObject _projectileParticle;

    private bool _canFire, _canShield;
    
    #endregion

    #region Events

    public UnityEvent OnFireReady   = new UnityEvent();
    public UnityEvent OnShieldReady = new UnityEvent();
    public UnityEvent OnFire = new UnityEvent();

    #endregion
    
    #region Unity LifeCycle

    private void Awake() => TryGetComponent(out _audioSource);

    #endregion

    #region Utility Methods

    public void SetPlayer(Player player)
    {
        _player = player;
        _shieldPrefab = player.PlayerId == Player.PlayerID.Player1 ? _redShield : _blueShield;
    }

    public void Fire()
    {
        if(!_canFire) return;
        
        var prefab = Instantiate(_magicProjectilePrefab, transform.position, Quaternion.identity);
        var magicProjectile = prefab.GetComponent<MagicProjectile>();

        magicProjectile.Initialize(_player, _projectileDamage, _projectileSpeed, _instaKillProbability, _poisonDamage, _projectileHealMultiplier, _projectileParticle, _sizeMultiplier);

        StartCoroutine(StartFireCooldown());
        OnFire?.Invoke();
    }

    public void Shield(Transform t)
    {
        if(!_canShield) return;
        
        var prefab = Instantiate(_shieldPrefab, t);
        var magicShield = prefab.GetComponent<MagicShield>();
        magicShield.Initialize(_player, _canReflect, _shieldHealMultiplier);

        StartCoroutine(StartShieldCooldown());
    }

    public void SetProjectileSpeed(float speed) => _projectileSpeed = speed;
    public void SetDamage(int damage) => _projectileDamage = damage;
    public void SetFireCooldown(float cooldown)
    {
        _projectileCooldown = cooldown;
        _canFire = true;
    }

    public void SetShieldCooldown(float cooldown)
    {
        _shieldCooldown = cooldown;
        _canShield = true;
    }

    public void SetInstaKillProb(int prob) => _instaKillProbability = prob;
    public void SetPoisonDamage(int dmg) => _poisonDamage = dmg;
    public void CanReflect(bool b) => _canReflect = b;
    public void SetProjectileSize(float multiplier) => _sizeMultiplier = multiplier;
    public void SetShieldHealMultiplier(float multiplier) => _shieldHealMultiplier = multiplier;
    public void SetProjectileHealMultiplier(float multiplier) => _projectileHealMultiplier = multiplier;
    
    private IEnumerator StartFireCooldown()
    {
        _canFire = false;
        _audioSource.PlayOneShot(_fireSFX);
        
        var t = 0f;
    
        while (t < 1)
        {
            t += Time.deltaTime / _projectileCooldown;

            _player.UpdateFireCooldownView(t);

            yield return null;
        }
        
        _canFire = true;
    }

    private IEnumerator StartShieldCooldown()
    {
        _canShield = false;
        _audioSource.PlayOneShot(_shieldSFX);
        
        var t = 0f;
    
        while (t < 1)
        {
            t += Time.deltaTime / _shieldCooldown;

            _player.UpdateShieldCooldownView(t);

            yield return null;
        }
        
        _canShield = true;
    }

    public void SetParticle(GameObject wandSoWandShot) => _projectileParticle = wandSoWandShot;

    #endregion
}
