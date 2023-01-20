using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FightGameController : MonoBehaviour
{
    #region Public Variables

    public static UnityEvent OnRoundEnded = new UnityEvent();

    #endregion
    
    #region Private Variables

    [SerializeField] private Transform _player1Spawn, _player2Spawn;
    [SerializeField] private FightUIController fightUIController;

    private Player _player1, _player2;
    private WizardSO _player1Wizard, _player2Wizard;
    private WandSO _player1Wand, _player2Wand;
    private  PersistentData _persistentData;
    private bool _winChecked;
    #endregion

    #region Unity LifeCycle

    private void Awake() => Initialize();

    #endregion

    #region Utility Methods

    private void Initialize()
    {
        _persistentData = PersistentData.Instance;
        
        _player1Wizard = _persistentData.WizardP1;
        _player2Wizard = _persistentData.WizardP2;
        _player1Wand = _persistentData.WandP1;
        _player2Wand = _persistentData.WandP2;

        _player1 = Instantiate(_player1Wizard.wizardPrefab, _player1Spawn).GetComponent<Player>();
        _player2 = Instantiate(_player2Wizard.wizardPrefab, _player2Spawn).GetComponent<Player>();
        
        _player1.Initialize(Player.PlayerID.Player1, this, _player1Wizard, _player1Wand);
        _player2.Initialize(Player.PlayerID.Player2, this, _player2Wizard, _player2Wand);
        
        fightUIController.Initialize(_player1Wizard, _player2Wizard);
    }
    
    public void OnPlayerHealthUpdated(Player player) => fightUIController.UpdatePlayerHealth(player);

    public void OnRoundEnd(Player loser)
    {
        if(_winChecked) return;

        _winChecked = true;
        
        var winner = loser.PlayerId == Player.PlayerID.Player1 ? _player2 : _player1;

        loser.TriggerDeathAnimation();
        winner.TriggerWinAnimation();
        
        _persistentData.SetRoundWinner(winner.PlayerId);

        StartCoroutine(OnEndWait());
        OnRoundEnded.Invoke();
    }

    #endregion

    public void OnShieldCooldownUpdated(Player p, float f) => fightUIController.UpdateShieldCooldown(p, f);

    public void OnFireCooldownUpdated(Player p, float f) => fightUIController.UpdateFireCooldown(p, f);

    private void OnGameEnd(Player loser) => fightUIController.OnGameEnd(loser);

    private IEnumerator OnEndWait()
    {
        var scene = SceneManager.GetActiveScene(); 
        yield return new WaitForSecondsRealtime(3.5f);
        
        switch (_persistentData.RoundNumber)
        {
            case 1:
                SceneManager.LoadScene(scene.name);
                break;
            case 2:
                if (_persistentData.Player1Rounds == _persistentData.Player2Rounds)
                {
                    SceneManager.LoadScene(scene.name);
                }
                else
                {
                    var gLoser = _persistentData.Player1Rounds > _persistentData.Player2Rounds ? _player2 : _player1;
                    OnGameEnd(gLoser);
                }
                break;
            case 3:
                var gameLoser = _persistentData.Player1Rounds > _persistentData.Player2Rounds ? _player2 : _player1;
                OnGameEnd(gameLoser);
                break;
            default:
                Debug.Log("Rondas: " + _persistentData.RoundNumber);
                break;
        }
    }
}