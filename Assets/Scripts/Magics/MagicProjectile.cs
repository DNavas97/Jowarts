using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    #region Private Variables
    
    private Rigidbody _rigidbody;
    private float _speed;
    private int _damage = 10;

    private Player.PlayerID _playerID;
    
    #endregion

    #region Unity LifeCycle

    private void Update()
    {
        _rigidbody.velocity = transform.right * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckWallCollision(other);
        CheckShieldCollision(other);
        CheckPlayerCollision(other);
    }

    #endregion

    #region Utility Methods

    private void CheckWallCollision(Collider other)
    {
        if(other.CompareTag("Wall")) Destroy(gameObject);
    }

    private void CheckShieldCollision(Collider other)
    {
        if (!other.CompareTag("Shield")) return;

        var shield = other.GetComponent<MagicShield>();

        if (shield.PlayerID == _playerID) return;
        
        if(!shield.CanReflect) Destroy(gameObject);
    }

    private void CheckPlayerCollision(Collider other)
    {
        var collidedPlayer = other.GetComponent<Player>();
        if(collidedPlayer == null || collidedPlayer.GetPlayerID() == _playerID) return;
            
        collidedPlayer.TakeDamage(_damage);
        Destroy(gameObject);
    }
    
    public void Initialize(Player.PlayerID player, int damage, float speed, float instaKillProb, int posionDamage)
    {
        _damage = damage;
        _playerID = player;
        _speed = speed;
        
        TryGetComponent(out _rigidbody);

        var speedDirectionMultiplier = _playerID == Player.PlayerID.Player1 ? 1 : -1;
        _speed = speed * speedDirectionMultiplier;
        
        transform.SetParent(null);
    }

    public Player.PlayerID PlayerID => _playerID;
    
    public void MirrorPlayer(Player.PlayerID player)
    {
        _playerID = player;
        _speed *= -1;
    }

    public int GetDamage() => _damage;

    #endregion
}