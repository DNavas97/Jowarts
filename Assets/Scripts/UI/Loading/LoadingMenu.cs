using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingMenu : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private LoadingPlayerInfo player1Info, player2Info;
    [SerializeField] private Transform _p1Spawn, _p2Spawn;

    private int _playersReady = 0;
    
    private WizardPreview _wizard1Preview;
    private WizardPreview _wizard2Preview;
    
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

        _wizard1Preview = Instantiate(persistenData.WizardP1.previewPrefab, _p1Spawn).GetComponent<WizardPreview>();
        _wizard2Preview = Instantiate(persistenData.WizardP2.previewPrefab, _p2Spawn).GetComponent<WizardPreview>();;
        
        player1Info.OnPlayerReady.AddListener(OnPlayerReady);
        player2Info.OnPlayerReady.AddListener(OnPlayerReady);

        persistenData.ResetCounter();
    }

    private void OnPlayerReady(Player.PlayerID playerID)
    {
        var playerPreview = playerID == Player.PlayerID.Player1 ? _wizard1Preview : _wizard2Preview;
        playerPreview.OnPlayerReady();
        
        _playersReady++;
        
        if(_playersReady < 2) return;

        StartCoroutine(OnStart());
    }

    private IEnumerator OnStart()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        
        SceneManager.LoadScene("PS_FightScene", LoadSceneMode.Single);
    }
    
    #endregion
}
