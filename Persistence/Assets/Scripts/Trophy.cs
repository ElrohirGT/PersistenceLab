using UnityEngine;
using Utils;

public class Trophy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventBus.OnGameEnd();
        }
    }
}
