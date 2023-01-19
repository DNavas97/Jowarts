using System.Collections;
using UnityEngine;

public class MagicShield : MonoBehaviour
{
    #region Public Variables

    public Player.PlayerID PlayerID;
    public bool CanReflect;
    
    #endregion
    
    #region Private Variables

    private Player _player;
    private const float SHIELD_TIME = 0.75f;
    private float _healMultiplier;
    
    #endregion

    #region Unity LifeCycle

    #endregion

    #region Utility Methods

    public void Initialize(Player player, bool canReflect, float healMultiplier)
    {
        _player = player;
        PlayerID = _player.PlayerId;
        CanReflect = canReflect;
        _healMultiplier = healMultiplier;
        
        StartCoroutine(DestroyCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Projectile")) return;

        var projectile = other.GetComponent<MagicProjectile>();
        if(projectile.PlayerID == PlayerID) return;
        
        if (CanReflect) projectile.MirrorPlayer(PlayerID);
        
        var healAmount = (int)(projectile.GetDamage() * _healMultiplier);
        if (healAmount != 0)
            _player.Heal(healAmount);
        
        Destroy(gameObject);
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSecondsRealtime(SHIELD_TIME);
        
        Destroy(gameObject);
    }

    #endregion
}