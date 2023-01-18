using UnityEngine;

public class MagicSpawner : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private GameObject _magicProjectilePrefab;
    [SerializeField] private GameObject _shieldPrefab;

    private Player _player;
    
    #endregion

    #region Unity LifeCycle
    

    #endregion

    #region Utility Methods

    public void Initialize(Player player)
    {
        _player = player;
    }
    
    public void Fire()
    {
        var prefab = Instantiate(_magicProjectilePrefab, transform.position, Quaternion.identity);
        var magicProjectile = prefab.GetComponent<MagicProjectile>();
        magicProjectile.Initialize(_player);
    }

    public void Shield(Transform t)
    {
        var prefab = Instantiate(_shieldPrefab, t);
        var magicShield = prefab.GetComponent<MagicShield>();
        magicShield.Initialize(_player.GetPlayerID());
    }

    #endregion
}
