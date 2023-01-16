using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private Image characterIcon;
    [SerializeField] private Transform healthContent;
    
    #endregion

    #region Unity LifeCycle

    #endregion

    #region Utility Methods

    public void UpdateHealthBar(int health)
    {
        var healthSize = health / 100f;
        healthContent.localScale = new Vector3(healthSize, 1, 1);
    }

    #endregion
}
