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

    public static UnityEvent OnFireReady   = new UnityEvent();
    public static UnityEvent OnShieldReady = new UnityEvent();

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
    public void SetProjectileSize(float multiplier) => _projectileSize = Vector3.one * multiplier;
    public void SetShieldHealMultiplier(float multiplier) => _shieldHealMultiplier = multiplier;
    public void SetProjectileHealMultiplier(float multiplier) => _projectileHealMultiplier = multiplier;
    
    private IEnumerator StartFireCooldown()
    {
        _canFire = false;
        yield return new WaitForSecondsRealtime(_projectileCooldown);
        _canFire = true;
    }

    private IEnumerator StartShieldCooldown()
    {
        _canShield = false;
        yield return new WaitForSecondsRealtime(_shieldCooldown);
        _canShield = true;
    }

    #endregion
}
