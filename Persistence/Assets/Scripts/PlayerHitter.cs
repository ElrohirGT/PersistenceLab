using UnityEngine;
using Utils;

public class PlayerHitter : MonoBehaviour
{
    [SerializeField] private int damage;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventBus.OnPlayerHit(damage);
        }
    }
}
