using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }

    private void UpdateLevelUI()
    {
        UIManager.Instance.UpdateLevelBar(_currentCollectableCount, _levelCollectableCount);
    }
}
