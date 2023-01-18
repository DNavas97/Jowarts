using System;
using System.Collections;
using UnityEngine;

public class MagicShield : MonoBehaviour
{
    #region Public Variables

    public Player.PlayerID PlayerID;

    #endregion
    
    #region Private Variables

    private const float SHIELD_TIME = 0.75f; 
    
    #endregion

    #region Unity LifeCycle

    #endregion

    #region Utility Methods

    public void Initialize(Player.PlayerID id)
    {
        PlayerID = id;
        StartCoroutine(DestroyCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Projectile")) return;
        
        if(other.GetComponent<MagicProjectile>().PlayerID == PlayerID) return;
        
        Destroy(gameObject);
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSecondsRealtime(SHIELD_TIME);
        
        Destroy(gameObject);
    }

    #endregion
}