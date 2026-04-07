using UnityEngine;
using Utils;

public class PickUp : MonoBehaviour
{
    [SerializeField] private string tutorial;
    [SerializeField] private int index;
    public int Index => index;

    public enum PickUpType
    {
        Unknown,
        Coin,
        Collectible,
    }

    [SerializeField] private PickUpType type;
    public PickUpType PickupType => type;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        switch (type)
        {
            case PickUpType.Coin:
                EventBus.OnCoinPickedUp(index);
                if (!string.IsNullOrEmpty(tutorial) && FirstCoinPickedUp(GameStateManager.Instance.State))
                {
                    GameStateManager.Instance.State.HasPickedCoinBefore = true;
                    EventBus.OnDisplayTutorial(tutorial);
                }
                break;
            case PickUpType.Collectible:
                EventBus.OnCollectiblePickedUp(index);
                if (!string.IsNullOrEmpty(tutorial) && FirstCollectible(GameStateManager.Instance.State))
                {
                    GameStateManager.Instance.State.HasPickedCollectibleBefore = true;
                    EventBus.OnDisplayTutorial(tutorial);
                }
                break;
        }

        Disable();
    }

    public void Disable()
    {
        var meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        enabled = false;
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
