using System.Collections;
using UnityEngine;

public class HandleFx : MonoBehaviour
{
    [SerializeField] private GameObject[] particles;

    private void Start()
    {
        GameManager.Instance.OnWaitingForNextLevel += () =>
        {
            for (int i = 0, length = particles.Length; i < length; i++)
            {
                particles[i].SetActive(true);
            }

            StartCoroutine(DisableParticles());
        };
    }

    private IEnumerator DisableParticles()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0, length = particles.Length; i < length; i++)
        {
            particles[i].SetActive(false);
        }
    }
}
