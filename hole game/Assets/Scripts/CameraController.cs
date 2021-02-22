using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeStrength = 0.8f;
    
    
    private void Start()
    {
        UIManager.Instance.OnRestartEffects += () => 
        Camera.main.DOShakeRotation(shakeDuration, strength:shakeStrength);
    }
}
