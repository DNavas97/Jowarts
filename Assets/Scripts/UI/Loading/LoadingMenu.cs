using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class LoadingMenu : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private LoadingPlayerInfo player1Info, player2Info;
    [SerializeField] private Transform _p1Spawn, _p2Spawn;
    [SerializeField] private AudioClip _vsClip;
    
    private int _playersReady = 0;
    private bool _canStart;
    
    private AudioSource _audioSource;
    private WizardPreview _wizard1Preview;
    private WizardPreview _wizard2Preview;
    private Coroutine _startCoroutine;
    
    #endregion

    #region Unity LifeCycle

    private void Awake() => TryGetComponent(out _audioSource);

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

        StartCoroutine(PlayVersusSFX());
        
        persistenData.ResetCounter();
    }

    private void OnPlayerReady(Player.PlayerID playerID)
    {
        var playerPreview = playerID == Player.PlayerID.Player1 ? _wizard1Preview : _wizard2Preview;
        playerPreview.OnPlayerReady();
        
        _playersReady++;
        
        if(_playersReady < 2 || !_canStart) return;

        if(_startCoroutine == null) _startCoroutine = StartCoroutine(OnStart());
    }

    private IEnumerator OnStart()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        
        var randomScene = Random.Range(1, 3);
        SceneManager.LoadScene($"PS_FightScene{randomScene}", LoadSceneMode.Single);
    }

    private IEnumerator PlayVersusSFX()
    {
        var p1 = PersistentData.Instance.WizardP1;
        var p2 = PersistentData.Instance.WizardP2;

        _audioSource.clip = p1.loadingSFX;
        _audioSource.Play();

        yield return new WaitForSecondsRealtime(p1.loadingSFX.length + 0.2f);
        
        _audioSource.clip = _vsClip;
        _audioSource.Play();
        
        yield return new WaitForSecondsRealtime(_vsClip.length + 0.2f);
        
        _audioSource.clip = p2.loadingSFX;
        _audioSource.Play();
        
        yield return new WaitForSecondsRealtime(p2.loadingSFX.length + 0.2f);
        
        _canStart = true;
        
        if(_playersReady == 2 && _startCoroutine == null) StartCoroutine(OnStart());
    }
    
    
    #endregion
}
