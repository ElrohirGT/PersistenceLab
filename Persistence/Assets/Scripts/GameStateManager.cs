using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        EventBus.GameEnd += EventBusOnGameEnd;
    }

    private void OnDisable()
    {
        EventBus.CoinPickedUp -= EventBusOnCoinPickedUp;
        EventBus.CollectiblePickedUp -= EventBusOnCollectiblePickedUp ;
        EventBus.UsernameChanged -= EventBusOnUsernameChanged;
        EventBus.GameEnd -= EventBusOnGameEnd;
    }
    
    
    private void EventBusOnGameEnd()
    {
        SceneManager.LoadScene("MainMenu");
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
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadState();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void LoadState()
    {
        var path = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log($"Trying to load from: {path}");
        if (!File.Exists(path))
        {
            Debug.LogError("Failed to load 'save.txt'! Initializing empty save...");
            State = new GameState();
        } else {
            var content = File.ReadAllText(path);
            State = JsonUtility.FromJson<GameState>(content);
        }
    }

    private void SaveToFile()
    {
        var json = JsonUtility.ToJson(State);
        var path = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log($"Saving to: {path}");
        File.WriteAllText(path, json);
    }

    public void DeleteSave()
    {
        var path = Path.Combine(Application.persistentDataPath, fileName);
        File.Delete(path);
        Instance.State.Reset();
    }
}
