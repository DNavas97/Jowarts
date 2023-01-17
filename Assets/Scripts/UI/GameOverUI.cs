using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI winnerText;
    
    #endregion

    #region Unity LifeCycle


    #endregion

    #region Utility Methods

    public void ShowWithWinner(Player player)
    {
        _canvasGroup.alpha = 1;
        SetWinnerText(player.name);
    }

    public void SetWinnerText(string winnerName)
    {
        winnerText.text = "Gana " + winnerName;
    }
    
    #endregion
}
