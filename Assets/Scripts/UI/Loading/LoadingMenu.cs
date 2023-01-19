using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingMenu : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private LoadingPlayerInfo player1Info, player2Info;
    [SerializeField] private Transform _p1Spawn, _p2Spawn;

    private int _playersReady = 0;
    
    #endregion

    #region Unity LifeCycle

    private void Start() => Initialize();

    #endregion

    #region Utility Methods

    private void Initialize()
    {
        var persistenData = PersistentData.Instance;

        player1Info.Initialize(persistenData.WizardP1, persistenData.WandP1);
        player2Info.Initialize(persistenData.WizardP2, persistenData.WandP2);

        Instantiate(persistenData.WizardP1.wizardPrefab, _p1Spawn);
        Instantiate(persistenData.WizardP2.wizardPrefab, _p2Spawn);
        
        player1Info.OnPlayerReady.AddListener(OnPlayerReady);
        player2Info.OnPlayerReady.AddListener(OnPlayerReady);

    }

    private void OnPlayerReady()
    {
        _playersReady++;
        
        if(_playersReady < 2) return;

        SceneManager.LoadScene("PS_FightScene", LoadSceneMode.Single);
    }
    
    #endregion
}
