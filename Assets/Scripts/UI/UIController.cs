using System;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    #region Public Variables

    public static UIController Instance;

    #endregion
    
    #region Private Variables

    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private HealthBar player1Bar, player2Bar;

    #endregion

    #region Unity LifeCycle

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    #endregion

    #region Utility Methods

    public void UpdatePlayerHealth(Player player)
    {
        var healthBar = player.GetPlayerID() == Player.PlayerID.Player1 ? player1Bar : player2Bar;
        var health = player.Health;

        healthBar.UpdateHealthBar(health);
    }

    public void SetWinnerText(string winnerName)
    {
        winnerText.text = "Gana " + winnerName;
    }

    #endregion
}
