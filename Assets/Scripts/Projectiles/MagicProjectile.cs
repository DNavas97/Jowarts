using System;
using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    #region Private Variables
    
    private Rigidbody _rigidbody;
    private float _speed;
    private int _damage = 10;

    private Player _ownPlayer;
    
    #endregion

    #region Unity LifeCycle

    private void Update()
    {
        _rigidbody.velocity = transform.right * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        var collidedPlayer = other.GetComponent<Player>();
        if(collidedPlayer == null || collidedPlayer == _ownPlayer) return;
            
        collidedPlayer.TakeDamage(_damage);
        Destroy(gameObject);
    }

    #endregion

    #region Utility Methods

    public void Initialize(Player player)
    {
        _ownPlayer = player;
        TryGetComponent(out _rigidbody);

        var speedDirectionMultiplier = player.GetPlayerID() == Player.PlayerID.Player1 ? 1 : -1;
        _speed = 15.0f * speedDirectionMultiplier;
        
        transform.SetParent(null);
    }
    
    #endregion
}