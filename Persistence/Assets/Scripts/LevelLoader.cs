using UnityEngine;
using Utils;

public class LevelLoader : MonoBehaviour
{
    private void Start()
    {
        var pickups = FindObjectsByType<PickUp>(FindObjectsSortMode.None);
        var state = GameStateManager.Instance.State;

        var shouldTeleport = false;
        Vector3 teleportLocation = default;
        if (state.Checkpoints.Count != 0)
        {
            var lastIdx = state.Checkpoints[-1];
            foreach (var pickup in pickups)
            {
                if (pickup.PickupType != PickUp.PickUpType.Coin || pickup.Index != lastIdx) continue;
                shouldTeleport = true;
                teleportLocation = pickup.transform.position;
            }
        }
        
        foreach (var cpIndex in state.Checkpoints)
        {
            foreach (var pickup in pickups)  
            {
                if (pickup.PickupType == PickUp.PickUpType.Coin && pickup.Index == cpIndex)
                {
                    Destroy(pickup.gameObject);
                }
            }
        }

        foreach (var collectIndex in state.PickedUpCollectibles)
        {
            foreach (var pickup in pickups)
            {
                if (pickup.PickupType == PickUp.PickUpType.Collectible && pickup.Index == collectIndex)
                {
                    Destroy(pickup.gameObject);
                }
            }
        }

        if (shouldTeleport)
        {
            EventBus.OnTeleportPlayer(teleportLocation);
        }
    }
}
