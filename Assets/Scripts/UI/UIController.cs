using UnityEngine;

public class UIController : MonoBehaviour
{
    #region Public Variables

    #endregion
    
    #region Private Variables

    [SerializeField] private GameOverUI _gameOverUI;
    [SerializeField] private HealthBar player1Bar, player2Bar;

    #endregion

    #region Unity LifeCycle

    #endregion

    #region Utility Methods

    public void UpdatePlayerHealth(Player player)
    {
        var healthBar = player.GetPlayerID() == Player.PlayerID.Player1 ? player1Bar : player2Bar;
        var health = player.Health;

        healthBar.UpdateHealthBar(health);
    }

    public void OnGameEnd(Player winner)
    {
        _gameOverUI.ShowWithWinner(winner);
    }

    #endregion
}
