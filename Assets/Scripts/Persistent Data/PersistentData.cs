using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentData : MonoBehaviour
{
    #region Public Variables

    public static PersistentData Instance;
    
    [HideInInspector] public WizardSO WizardP1, WizardP2;
    [HideInInspector] public WandSO WandP1, WandP2;

    private WizardDB _wizardDB;
    private WandDB   _wandDB;
    
    private const string CharacterDBPath = "SO_WizardDB";
    private const string WandDBPath = "SO_WandDB";
    
    #endregion

    #region Private Variables

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
        LoadDatabase();
    }

    #endregion

    #region Unity LifeCycle
    

    #endregion

    #region Utility Methods

    private void LoadDatabase()
    {
        _wizardDB = Resources.Load<WizardDB>(CharacterDBPath);
        _wandDB = Resources.Load<WandDB>(WandDBPath);    
    }

    public void SetMatchData(int wizardP1, int wizardP2, int wandP1, int wandP2)
    {
        WizardP1 = _wizardDB.wizards[wizardP1];
        WizardP2 = _wizardDB.wizards[wizardP2];
        
        WandP1 = _wandDB.wands[wandP1];
        WandP2 = _wandDB.wands[wandP2];
        
        SceneManager.LoadScene("FightScene", LoadSceneMode.Single);
    }

    public List<WizardSO> GetWizardDB() => _wizardDB.wizards;
    public List<WandSO> GetWandDB() => _wandDB.wands;

    #endregion
}
