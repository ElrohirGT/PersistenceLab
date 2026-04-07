using System;
using UnityEngine;
using Utils;

public class LevelLoader : MonoBehaviour
{
    private void OnEnable()
    {
        EventBus.PlayerHit += EventBusOnPlayerHit;
    }

    private void OnDisable()
    {
        EventBus.PlayerHit -= EventBusOnPlayerHit;
    }

    private void EventBusOnPlayerHit(int obj)
    {
        Debug.Log("Spawning player!");
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        var pickups = FindObjectsByType<PickUp>(FindObjectsSortMode.None);
        var state = GameStateManager.Instance.State;
        if (state.Checkpoints.Count == 0) return;

        var lastIdx = state.Checkpoints[^1];
        foreach (var pickup in pickups)
        {
            if (pickup.PickupType != PickUp.PickUpType.Coin || pickup.Index != lastIdx) continue;
            var position = pickup.transform.position;
            EventBus.OnTeleportPlayer(position);
        }
    }
    
    private void Start()
    {
        var pickups = FindObjectsByType<PickUp>(FindObjectsSortMode.None);
        var state = GameStateManager.Instance.State;

        foreach (var cpIndex in state.Checkpoints)
        {
            foreach (var pickup in pickups)  
            {
                if (pickup.PickupType == PickUp.PickUpType.Coin && pickup.Index == cpIndex)
                {
                    pickup.Disable();
                }
            }
        }

        foreach (var collectIndex in state.PickedUpCollectibles)
        {
            foreach (var pickup in pickups)
            {
                if (pickup.PickupType == PickUp.PickUpType.Collectible && pickup.Index == collectIndex)
                {
                    pickup.Disable();
                }
            }
        }

        SpawnPlayer();
    }
}
