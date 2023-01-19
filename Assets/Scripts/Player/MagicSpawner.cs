using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MagicSpawner : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private GameObject _magicProjectilePrefab;
    [SerializeField] private GameObject _shieldPrefab;

    private Player _player;
    private int   _projectileDamage;
    private float _projectileSpeed;
    private float _projectileCooldown;
    private float _shieldCooldown;
    private int _instaKillProbability;
    private bool  _canReflect;
    private int   _poisonDamage;
    private Vector3 _projectileSize;
    private float _projectileHealMultiplier;
    private float _shieldHealMultiplier;

    private bool _canFire, _canShield;
    
    #endregion

    #region Events

    public UnityEvent OnFireReady   = new UnityEvent();
    public UnityEvent OnShieldReady = new UnityEvent();
    public UnityEvent OnFire = new UnityEvent();

    #endregion
    
    #region Unity LifeCycle
    

    #endregion

    #region Utility Methods

    public void SetPlayer(Player player) => _player = player;

    public void Fire()
    {
        if(!_canFire) return;
        
        var prefab = Instantiate(_magicProjectilePrefab, transform.position, Quaternion.identity);
        var magicProjectile = prefab.GetComponent<MagicProjectile>();
        prefab.transform.localScale = _projectileSize;
        
        magicProjectile.Initialize(_player, _projectileDamage, _projectileSpeed, _instaKillProbability, _poisonDamage, _projectileHealMultiplier);

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
    public void SetProjectileSize(float multiplier) => _projectileSize = new Vector3(0.1f, 0.1f, 0.1f) * multiplier;
    public void SetShieldHealMultiplier(float multiplier) => _shieldHealMultiplier = multiplier;
    public void SetProjectileHealMultiplier(float multiplier) => _projectileHealMultiplier = multiplier;
    
    private IEnumerator StartFireCooldown()
    {
        _canFire = false;
        
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
        
        var t = 0f;
    
        while (t < 1)
        {
            t += Time.deltaTime / _shieldCooldown;

            _player.UpdateShieldCooldownView(t);

            yield return null;
        }
        
        _canShield = true;
    }

    #endregion
}
