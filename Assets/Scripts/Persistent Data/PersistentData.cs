using UnityEngine;

public class PersistentData : MonoBehaviour
{
    #region Public Variables

    public static PersistentData Instance;
    
    [HideInInspector] public WizardSO WizardP1, WizardP2;
    [HideInInspector] public WandSO WandP1, WandP2;

    #endregion

    #region Private Variables

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
    }

    #endregion

    #region Unity LifeCycle
    

    #endregion

    #region Utility Methods

    public void SetPlayer1Wizard(WizardSO wizard) => WizardP1 = wizard;
    public void SetPlayer2Wizard(WizardSO wizard) => WizardP2 = wizard;
    
    public void SetPlayer1Wand(WandSO wand) => WandP1 = wand;
    public void SetPlayer2Wand(WandSO wand) => WandP2 = wand;

    #endregion
}
