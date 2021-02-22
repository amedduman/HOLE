using UnityEngine;


public class HoleTrigger : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("damaging"))
        {
            GameManager.Instance.RestartLevel();
        }
        
        else if (other.gameObject.CompareTag("collectable"))
        {
            GameManager.Instance.DecreaseCollectableCount();
        }
    }
}
