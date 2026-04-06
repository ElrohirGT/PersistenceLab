using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Utils;

public class MainMenuController : MonoBehaviour
{
    private VisualElement _root;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var doc = GetComponent<UIDocument>();
        _root = doc.rootVisualElement;
        _root.Q<TextField>().value = GameStateManager.Instance.State.Username;

        _root.Q<Button>("btnPlay").clicked += () =>
        {
            var userName = _root.Q<TextField>().text;
            EventBus.OnUsernameChanged(userName);
            SceneManager.LoadScene("LevelScene");
        };

        _root.Q<Button>("btnDelete").clicked += () =>
        {
            GameStateManager.Instance.DeleteSave();
        };

        _root.Q<Button>("btnQuit").clicked += UT.QuitGame;
    }
}
