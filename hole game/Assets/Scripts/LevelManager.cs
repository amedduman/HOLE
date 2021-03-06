using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;

    public static LevelManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("there is no level manager at the scene");
            return _instance;
        }
    }
    
    [System.Serializable]
    private class LevelData
    {
        public Level level;

        [ReadOnlyInspector] public int collectableCount;
    }

    [ContextMenu("Update list")]
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
        if (_instance == null)
        {
            _instance = this;
        }
        else if(_instance != this)
        {
            Destroy(this.gameObject);
        }

        _currentLevelIndex = PlayerPrefs.GetInt("_savedLevel");
        LoadCurrentLevel();
    }

    public int GetCollectableCount()
    {
        var count = levels[_currentLevelIndex].collectableCount;
        return count;
    }

    private void LoadCurrentLevel()
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
            SaveLevelIndex(_currentLevelIndex);
            LoadCurrentLevel();
        }
        else
        {
            _currentLevelIndex = 0;
            SaveLevelIndex(_currentLevelIndex);
            LoadCurrentLevel();
        }
    }

    public void RestartLevel()
    {
        DOTween.PauseAll();
        SceneManager.LoadScene(0);
    }
    
    private void DisableCurrentLevel()
    {
        var currentLevel = levels[_currentLevelIndex];
        currentLevel.level.gameObject.SetActive(false);
    }

    private void SaveLevelIndex(int currentIndex)
    {
        PlayerPrefs.SetInt("_savedLevel", currentIndex);
        PlayerPrefs.Save();
    }
}
