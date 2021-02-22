using UnityEngine;

public class DestroyFallenCubes : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        other.gameObject.GetComponent<MeshRenderer>().enabled = false;
    }
}
