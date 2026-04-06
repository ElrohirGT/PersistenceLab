using System;
using UnityEngine;
using Utils;

public class PickUp : MonoBehaviour
{
    [SerializeField] private string tutorial;
    [SerializeField] private int index;

    enum PickUpType
    {
        Unknown,
        Coin,
        Collectible,
    }

    [SerializeField] private PickUpType type;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        switch (type)
        {
            case PickUpType.Coin:
                EventBus.OnCoinPickedUp(index);
                break;
            case PickUpType.Collectible:
                EventBus.OnCollectiblePickedUp(index);
                break;
        }

        Func<GameState, bool> checker = type switch
        {
            PickUpType.Coin => FirstCoinPickedUp,
            PickUpType.Collectible => FirstCollectible,
            _ => NeverShowTutorial
        };

        if (!string.IsNullOrEmpty(tutorial) && checker(GameStateManager.Instance.State))
        {
            EventBus.OnDisplayTutorial(tutorial);
        }
        
        Destroy(gameObject);
    }

    public static bool FirstCoinPickedUp(GameState state)
    {
        return !state.HasPickedCoinBefore;
    }

    public static bool FirstCollectible(GameState state)
    {
        return !state.HasPickedCollectibleBefore;
    }

    public static bool NeverShowTutorial(GameState state)
    {
        return false;
    }
}
