using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private Text text;
    
    
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

    public void UpdateLevelBar(int CollectableCount)
    {
        text.text = CollectableCount.ToString();
    }
}
