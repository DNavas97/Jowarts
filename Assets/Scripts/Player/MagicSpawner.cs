using UnityEngine;

public class MagicSpawner : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private GameObject _magicProjectilePrefab;

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

    #endregion
}
