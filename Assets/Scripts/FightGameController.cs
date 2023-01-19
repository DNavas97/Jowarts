using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FightGameController : MonoBehaviour
{
    #region Public Variables

    public static FightGameController Instance;
    public static UnityEvent OnGameEnded = new UnityEvent();

    #endregion
    
    #region Private Variables

    [SerializeField] private Player _player1, _player2;
    [SerializeField] private FightUIController fightUIController;

    private WizardSO _player1Wizard, _player2Wizard;
    private WandSO _player1Wand, _player2Wand;

    #endregion

    #region Unity LifeCycle

    private void Awake()
    {
        if (Instance != null) Instance = this;
        else Destroy(this);
        
        Initialize();
    }

    #endregion

    #region Utility Methods

    private void Initialize()
    {
        var data = PersistentData.Instance;
        
        _player1Wizard = data.WizardP1;
        _player2Wizard = data.WizardP2;
        _player1Wand = data.WandP1;
        _player2Wand = data.WandP2;
        
        _player1.Initialize(this, _player1Wizard, _player1Wand);
        _player2.Initialize(this, _player2Wizard, _player2Wand);
        
        fightUIController.Initialize(_player1Wizard, _player2Wizard);
    }
    
    public void OnPlayerHealthUpdated(Player player) => fightUIController.UpdatePlayerHealth(player);

    public void OnGameEnd(Player loser)
    {
        var winner = loser == _player1 ? _player2 : _player1;
        
        OnGameEnded.Invoke();
        fightUIController.OnGameEnd(winner);
    }

    #endregion

    public void OnShieldCooldownUpdated(Player p, float f) => fightUIController.UpdateShieldCooldown(p, f);

    public void OnFireCooldownUpdated(Player p, float f) => fightUIController.UpdateFireCooldown(p, f);

}