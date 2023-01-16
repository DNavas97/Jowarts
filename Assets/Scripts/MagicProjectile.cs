using System;
using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    #region Private Variables
    
    private Rigidbody m_Rigidbody;
    private float m_Speed;

    private Player _ownPlayer;
    
    #endregion

    #region Unity LifeCycle

    private void Update()
    {
        m_Rigidbody.velocity = transform.right * m_Speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        var collidedPlayer = other.GetComponent<Player>();
        if(collidedPlayer == null || collidedPlayer == _ownPlayer) return;
            
        Destroy(gameObject);
    }

    #endregion

    #region Utility Methods

    public void Initialize(Player player)
    {
        _ownPlayer = player;
        TryGetComponent(out m_Rigidbody);
        
        m_Speed = 10.0f;
        transform.SetParent(null);
    }
    
    #endregion
}