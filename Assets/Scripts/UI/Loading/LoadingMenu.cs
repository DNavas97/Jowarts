using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingMenu : MonoBehaviour
{
    #region Private Variables

    [SerializeField] private LoadingPlayerInfo player1Info, player2Info;
    [SerializeField] private Transform _barContent;
    private PersistentData _data;
    
    #endregion

    #region Unity LifeCycle

    private void Awake() => _data = PersistentData.Instance;

    private void Start() => Initialize();

    #endregion

    #region Utility Methods

    private void Initialize()
    {
        StartCoroutine(LoadBar());
    }

    private IEnumerator LoadBar()
    {
        var t = 0f;
        const float loadTime = 5f;
        
        while (t < 1)
        {
            t += Time.deltaTime / loadTime;
            
            _barContent.localScale = new Vector3(t, 1, 1);
            
            yield return null;
        }
        
        OnLoadedEnd();
    }
    
    private void OnLoadedEnd() => SceneManager.LoadScene("PS_FightScene", LoadSceneMode.Single);

    #endregion
}
