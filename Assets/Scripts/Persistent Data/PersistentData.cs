using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentData : MonoBehaviour
{
    #region Public Variables

    [SerializeField] private AudioClip _fluteFailMusic;
    [SerializeField] private AudioClip _battleMusic;
    [SerializeField] private AudioClip _menuMusic;
    
    public static PersistentData Instance;
    
    public int RoundNumber { get; set; }
    public int Player1Rounds { get; set; }
    public int Player2Rounds { get; set; }
    
    [HideInInspector] public WizardSO WizardP1, WizardP2;
    [HideInInspector] public WandSO WandP1, WandP2;
    
    #endregion

    #region Private Variables

    private AudioSource _audioSource;
    
    private const string CharacterDBPath = "SO_WizardDB";
    private const string WandDBPath = "SO_WandDB";
    
    private WizardDB _wizardDB;
    private WandDB   _wandDB;

    #endregion

    #region Unity LifeCycle
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        TryGetComponent(out _audioSource);
        LoadDatabase();
        CheckMusic(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        
        SceneManager.sceneLoaded += CheckMusic;
    }

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
        
        SceneManager.LoadScene("PS_Loading", LoadSceneMode.Single);
    }

    public List<WizardSO> GetWizardDB() => _wizardDB.wizards;
    public List<WandSO> GetWandDB() => _wandDB.wands;

    public void ResetCounter() => RoundNumber = Player1Rounds = Player2Rounds = 0;

    public void SetRoundWinner(Player.PlayerID player)
    {
        RoundNumber++;
        
        if (player == Player.PlayerID.Player1) Player1Rounds++;
        else                                   Player2Rounds++;
    }

    private void CheckMusic(Scene arg0, LoadSceneMode loadSceneMode)
    {
        var currentMusic = _audioSource.clip;
        
        switch (arg0.name)
        {
            case "PS_Title":
                _audioSource.clip = _fluteFailMusic;
                _audioSource.Play();
                break;
            case "PS_Lobby":
                _audioSource.clip = _menuMusic;
                _audioSource.Play();
                break;
            case "PS_Loading":
                if (currentMusic != _menuMusic)
                {
                    _audioSource.clip = _menuMusic;
                    _audioSource.Play();
                }
                break;
            case "PS_FightScene":
                if (currentMusic != _battleMusic)
                {
                    _audioSource.clip = _battleMusic;  
                    _audioSource.Play();
                }
                break;
            default:
                _audioSource.clip = _menuMusic;
                _audioSource.Play();
                break;
        }
    }

    public IEnumerator StartFade(float targetVolume)
    {
        float currentTime = 0;
        var start = _audioSource.volume;
        
        while (currentTime < 1f)
        {
            currentTime += Time.deltaTime;
            _audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / 1f);
            yield return null;
        }
    }

    public void SetMusicVolume(float targetVolume) => StartCoroutine(StartFade(targetVolume));

    #endregion
}
