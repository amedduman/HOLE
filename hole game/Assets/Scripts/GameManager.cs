using System;
using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null) Debug.LogError( "there is no game manager at scene");
            return _instance;
        }
    }

    public event Action OnLoadNextLevel;
    public event Action OnWaitingForNextLevel;
    public event Action OnWaitingRestartLevel;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if(_instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    private int _currentCollectableCount;
    private int _levelCollectableCount;
    private void Start()
    {
        Application.targetFrameRate = 30;
        
        _currentCollectableCount = LevelManager.Instance.GetCollectableCount();
        _levelCollectableCount = LevelManager.Instance.GetCollectableCount();
        UpdateLevelUI();
    }

    public void DecreaseCollectableCount()
    {
        _currentCollectableCount--;
        UpdateLevelUI();
        if (_currentCollectableCount == 0)
        {
            OnWaitingForNextLevel?.Invoke();
            UIManager.Instance.ShowEndLevelScreen();
        }
    }

    public void HandleNextLevel()
    {
        OnLoadNextLevel?.Invoke();
        LevelManager.Instance.LoadNextLevel();
        _currentCollectableCount = LevelManager.Instance.GetCollectableCount();
        _levelCollectableCount = LevelManager.Instance.GetCollectableCount();
        UpdateLevelUI();
    }

    public void RestartLevel()
    {
        DOTween.PauseAll();
        SceneManager.LoadScene(0);
        // _currentCollectableCount = _levelCollectableCount;
        // OnWaitingRestartLevel?.Invoke();
        // OnLoadNextLevel?.Invoke();
        // UIManager.Instance.HandleRestart();
        // LevelManager.Instance.ResetProcess();
    }

    private void UpdateLevelUI()
    {
        UIManager.Instance.UpdateLevelBar(_currentCollectableCount, _levelCollectableCount);
    }
}
