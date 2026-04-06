using System.IO;
using UnityEngine;
using Utils;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }
    public GameState State { get; private set; }

    [SerializeField] private string fileName = "save.txt";

    private void OnEnable()
    {
        EventBus.CoinPickedUp += EventBusOnCoinPickedUp;
        EventBus.CollectiblePickedUp += EventBusOnCollectiblePickedUp ;
        EventBus.UsernameChanged += EventBusOnUsernameChanged;
    }

    
    private void OnDisable()
    {
        EventBus.CoinPickedUp -= EventBusOnCoinPickedUp;
        EventBus.CollectiblePickedUp -= EventBusOnCollectiblePickedUp ;
        EventBus.UsernameChanged -= EventBusOnUsernameChanged;
    }
    
    private void EventBusOnCoinPickedUp(int obj)
    {
        State.Checkpoints.Add(obj);
        SaveToFile();
    }
    private void EventBusOnCollectiblePickedUp(int obj)
    {
        State.PickedUpCollectibles.Add(obj);
        SaveToFile();
    }
    private void EventBusOnUsernameChanged(string obj)
    {
        State.Username = obj;
        SaveToFile();
    }

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        LoadState();
        var pickups = FindObjectsByType<PickUp>(FindObjectsSortMode.None);
    }

    private void LoadState()
    {
        var path = Path.Combine(Application.persistentDataPath, fileName);
        if (!File.Exists(path))
        {
            Debug.LogError("Failed to load 'save.txt'! Initializing empty save...");
        } else {
            var content = File.ReadAllText(path);
            State = JsonUtility.FromJson<GameState>(content);
        }
    }

    private void SaveToFile()
    {
        var json = JsonUtility.ToJson(State);
        var path = Path.Combine(Application.persistentDataPath, fileName);
        File.WriteAllText(path, json);
    }

    public void DeleteSave()
    {
        var path = Path.Combine(Application.persistentDataPath, fileName);
        File.Delete(path);
    }
}
