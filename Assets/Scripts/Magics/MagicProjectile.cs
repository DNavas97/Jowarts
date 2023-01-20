using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    #region Public Variables

    #endregion
    
    #region Private Variables
    
    [SerializeField] private Transform _projectileSpawn;
    [SerializeField] private GameObject _instakillParticle;
    
    private Rigidbody _rigidbody;
    private float _speed;
    private int _damage = 10;
    private bool _instaKill = false;
    private int _poisonDamage;
    private float _healMultiplier;

    private Player.PlayerID _playerID;
    private Player _ownPlayer;
    
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
        if(collidedPlayer == null || collidedPlayer.PlayerId == _playerID) return;
        
        if(_poisonDamage != 0) collidedPlayer.GetPoisoned(_poisonDamage);
        if (_healMultiplier > 0)
        {
            var healAmount = (int)(_damage * _healMultiplier);
            _ownPlayer.Heal(healAmount);
        }
        var damage = _instaKill ? 9999 : _damage;
        collidedPlayer.TakeDamage(damage, false);
        
        Destroy(gameObject);
    }
    
    public void Initialize(Player player, int damage, float speed, int instaKillProb, int posionDamage, float healMultiplier, GameObject prefab, float sizeMultiplier)
    {
        _damage = damage;
        _ownPlayer = player;
        _playerID = _ownPlayer.PlayerId;
        _speed = speed;
        _poisonDamage = posionDamage;
        _healMultiplier = healMultiplier;
        
        transform.localScale *= sizeMultiplier;
        
        var instakill = Random.Range(0, 100 + 1);
        var isInstakill = instakill <= instaKillProb;
        
        if (isInstakill) OnInstakillSpawned();

        var particlePrefab = isInstakill ? _instakillParticle : prefab;
        var particle = Instantiate(particlePrefab, _projectileSpawn);
        var particleMultiplier = sizeMultiplier == 1 ? sizeMultiplier : sizeMultiplier * 3;
        particle.transform.localScale *=  sizeMultiplier * particleMultiplier;

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

    private void OnInstakillSpawned()
    {
        //Avada Kedaavra
        _instaKill = true;
    }

    #endregion
}