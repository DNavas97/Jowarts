using UnityEngine;

public class FightUIController : MonoBehaviour
{
    #region Public Variables

    #endregion
    
    #region Private Variables

    [SerializeField] private GameOverUI _gameOverUI;
    [SerializeField] private CountdownMenu _countdownMenu;
    [SerializeField] private StatsContent player1Bar, player2Bar;

    #endregion

    #region Unity LifeCycle

    #endregion

    #region Utility Methods

    public void Initialize(WizardSO wizard1, WizardSO wizard2)
    {
        _countdownMenu.Initialize();
        
        UpdatePlayerIcon(Player.PlayerID.Player1, wizard1);
        UpdatePlayerIcon(Player.PlayerID.Player2, wizard2);
    }

    private void UpdatePlayerIcon(Player.PlayerID player, WizardSO wizard)
    {
        var playerBar = player == Player.PlayerID.Player1 ? player1Bar : player2Bar;
        
        playerBar.UpdatePlayerInfo(wizard);
    }
    
    public void UpdatePlayerHealth(Player player)
    {
        var healthBar = player.GetPlayerID() == Player.PlayerID.Player1 ? player1Bar : player2Bar;
        var health = player.CurrentHealth;

        healthBar.UpdateHealthBar(health, player.MaxHealth);
    }

    public void OnGameEnd(Player winner)
    {
        _gameOverUI.ShowWithWinner(winner);
    }

    #endregion

    public void UpdateShieldCooldown(Player player, float f)
    {
        var healthBar = player.GetPlayerID() == Player.PlayerID.Player1 ? player1Bar : player2Bar;
        healthBar.UpdateShieldCooldownBar(f);
    }

    public void UpdateFireCooldown(Player player, float f)
    {
        var healthBar = player.GetPlayerID() == Player.PlayerID.Player1 ? player1Bar : player2Bar;
        healthBar.UpdateFireCooldownBar(f);
    }
}
