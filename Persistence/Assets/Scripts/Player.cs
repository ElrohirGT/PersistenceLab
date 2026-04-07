using System;
using UnityEngine;
using Utils;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        EventBus.TeleportPlayer += EventBusOnTeleportPlayer;
    }

    private void OnDisable()
    {
        EventBus.TeleportPlayer -= EventBusOnTeleportPlayer;
    }

    private void EventBusOnTeleportPlayer(Vector3 obj)
    {
        _controller.enabled = false;
        gameObject.transform.position = obj;
        _controller.enabled = true;
    }
}
