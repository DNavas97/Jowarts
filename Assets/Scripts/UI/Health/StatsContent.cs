using System.Collections.Generic;
using EWorldsCore.Base.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsContent : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private Image wizardIcon;
    [SerializeField] private Transform healthContent, manaContent, shieldContent;
    [SerializeField] private TextMeshProUGUI wizardNameText;
    [SerializeField] private List<Image> _roundImages;
    
    private const int N = 4;
    private const float M = 1.5f;
    
    #endregion

    #region Unity LifeCycle

    #endregion

    #region Utility Methods

    public void UpdateHealthBar(int health, int maxHealth)
    {
        var healthCalc = health / (float)maxHealth * 100f;
        var nRoot = Mathf.Pow((healthCalc / 100f), (1f / M));
        var nPower = Mathf.Pow((healthCalc / 100f), N);

        var healthImage = healthContent.GetComponent<Image>();
        healthImage.color = health < maxHealth / 2 ? Color.Lerp(Color.red, Color.yellow, (float)nRoot) : Color.Lerp(Color.yellow, Color.green, (float)nPower);

        var healthSize = health / (float)maxHealth;
        healthContent.localScale = new Vector3(healthSize, 1, 1);
    }

    public void UpdateFireCooldownBar(float f) => manaContent.localScale = new Vector3(f, 1, 1);

    public void UpdateShieldCooldownBar(float f) => shieldContent.localScale = new Vector3(1, f, 1);

    public void UpdatePlayerInfo(WizardSO wizard)
    {
        wizardNameText.text = EnumUtils.GetEnumDescription(wizard.wizardName);
        wizardIcon.sprite = wizard.wizardIcon;
    }

    public void UpdateRounds(int n)
    {
        for(var i = 0; i < n; i ++)
            _roundImages[i].color = Color.cyan;
    }
    
    #endregion
}
