using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor.Build.Content;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if(_instance == null) Debug.LogError( "there is no ui manager at scene");
            return _instance;
        }
    }

    [SerializeField] private Image levelBar;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private float levelBarFillDuration = 0.3f;
    [SerializeField] private float endScreenShowUpDelay = 0.6f;
    [SerializeField] private Image panel;

    public event Action OnRestartEffects;
    
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

        playAgainButton.gameObject.SetActive(false);
    }

    public void UpdateLevelBar(int collectableCount, int maxCollectableCount )
    {
        var levelPercent = 1 - ((float) collectableCount / maxCollectableCount);
        levelBar.DOFillAmount(levelPercent, levelBarFillDuration);
    }

    private void ResetLevelBar()
    {
        levelBar.fillAmount = 0;
        levelBar.DOFillAmount(0, 0);
    }

    public void ShowEndLevelScreen()
    {
        StartCoroutine(DelayForShowingEndScreen());
    }

    private IEnumerator DelayForShowingEndScreen()
    {
        yield return new WaitForSeconds(endScreenShowUpDelay);
        playAgainButton.gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        GameManager.Instance.HandleNextLevel();
        playAgainButton.gameObject.SetActive(false);
        ResetLevelBar();
    }

    public void HandleRestart()
    {
        ResetLevelBar();
        panel.color = Color.white;
        OnRestartEffects?.Invoke();
        panel.DOFade(0, 1);
    }
}
