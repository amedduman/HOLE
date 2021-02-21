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
    private void Start()
    {
        _currentCollectableCount = LevelManager.Instance.GetCollectableCount();
        UpdateLevelUI();
    }

    public void DecreaseCollectableCount()
    {
        if (_currentCollectableCount > 1)
        {
            _currentCollectableCount--;
            UpdateLevelUI();
        }
        else
        {
            HandleNextLevel();
        }
    }

    private void HandleNextLevel()
    {
        OnLoadNextLevel?.Invoke();
        LevelManager.Instance.LoadNextLevel();
        _currentCollectableCount = LevelManager.Instance.GetCollectableCount();
        UpdateLevelUI();
    }

    private void UpdateLevelUI()
    {
        UIManager.Instance.UpdateLevelBar(_currentCollectableCount);
    }
}
