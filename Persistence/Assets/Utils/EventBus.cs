using System;
using UnityEngine;

namespace Utils
{
    public static class EventBus
    {
        public static event Action<int> PlayerHit;
        public static void OnPlayerHit(int damage)
        {
            PlayerHit?.Invoke(damage);
            Debug.Log($"Player got damaged! {damage}");
        }

        public static event Action<int> CoinPickedUp;
        public static void OnCoinPickedUp(int coinIdx)
        {
            CoinPickedUp?.Invoke(coinIdx);
        }

        public static event Action<int> CollectiblePickedUp;
        public static void OnCollectiblePickedUp(int pickUpIdx)
        {
            CollectiblePickedUp?.Invoke(pickUpIdx);
        }

        public static event Action<string> DisplayTutorial;
        public static void OnDisplayTutorial(string tutorial)
        {
            Debug.Log($"Display tutorial: {tutorial}");
            DisplayTutorial?.Invoke(tutorial);
        }

        public static event Action GameEnd;
        public static void OnGameEnd()
        {
            GameEnd?.Invoke();
        }

        public static event Action<string> UsernameChanged;
        public static void OnUsernameChanged(string obj)
        {
            UsernameChanged?.Invoke(obj);
        }

        public static event Action<Vector3> TeleportPlayer;
        public static void OnTeleportPlayer(Vector3 obj)
        {
            TeleportPlayer?.Invoke(obj);
        }
    }
}