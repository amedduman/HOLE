using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _levelManager;

    public static LevelManager Instance
    {
        get
        {
            if (_levelManager == null) Debug.Log("there is no level manager at the scene");
            return _levelManager;
        }
    }
    
    [System.Serializable]
    private class LevelData
    {
        public Level level;

        [ReadOnlyInspector] public int collectableCount;
    }

    public void UpdateLevelListInfo()
    {
        for (int i = 0, length = levels.Count; i < length; i++)
        {
            // need to be reset otherwise it will ad values
            levels[i].collectableCount = 0; 
            foreach (Transform child in levels[i].level.transform)
            {
                if (child.CompareTag("collectable"))
                    levels[i].collectableCount++;
            }
        }
    }

    [SerializeField] private List<LevelData>  levels;
     private int _currentLevelIndex = 0;
     
    private void Awake()
    {
        if (_levelManager == null)
        {
            _levelManager = this;
        }
        else if(_levelManager != this)
        {
            Destroy(this.gameObject);
        }
        
        LoadCurrentLevel();
    }

    public void LoadCurrentLevel()
    {
        var currentLevel = levels[_currentLevelIndex];
        currentLevel.level.gameObject.SetActive(true);
    }

    public void LoadNextLevel()
    {
        DisableCurrentLevel();
        
        if (_currentLevelIndex < levels.Count - 1)
        {
            _currentLevelIndex++;
            LoadCurrentLevel();
        }
        else
        {
            _currentLevelIndex = 0;
            LoadCurrentLevel();
        }
    }
    
    private void DisableCurrentLevel()
    {
        var currentLevel = levels[_currentLevelIndex];
        currentLevel.level.gameObject.SetActive(false);
    }

    public void ResetLevel()
    {
        DisableCurrentLevel();
        LoadCurrentLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ResetLevel();
        }
    }
}
