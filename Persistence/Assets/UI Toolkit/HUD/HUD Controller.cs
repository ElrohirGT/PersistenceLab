using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using Utils;

public class HUDController : MonoBehaviour
{
    [SerializeField] private float tutorialDelaySeconds = 5;
    private float _timer;
    private Label _usernameLabel;
    private Label _tutorialLabel;

    private void OnEnable()
    {
        EventBus.UsernameChanged += EventBusOnUsernameChanged;
        EventBus.DisplayTutorial += EventBusOnDisplayTutorial;
    }

    private void OnDisable()
    {
        EventBus.UsernameChanged -= EventBusOnUsernameChanged;
        EventBus.DisplayTutorial -= EventBusOnDisplayTutorial;
    }
    
    private void EventBusOnDisplayTutorial(string obj)
    {
        _tutorialLabel.text = obj;
        _tutorialLabel.style.display = DisplayStyle.Flex;
        _timer = tutorialDelaySeconds;
    }

    private void EventBusOnUsernameChanged(string obj)
    {
        _usernameLabel.text = obj;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var doc = GetComponent<UIDocument>();
        _usernameLabel = doc.rootVisualElement.Q<Label>("nameDisplay");
        _usernameLabel.text = GameStateManager.Instance.State.Username;

        _tutorialLabel = doc.rootVisualElement.Q<Label>("tutorial");
        _tutorialLabel.style.display = DisplayStyle.None;
    }

    private void Update()
    {
        if (_timer>0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            _tutorialLabel.style.display = DisplayStyle.None;
        }
    }
}
