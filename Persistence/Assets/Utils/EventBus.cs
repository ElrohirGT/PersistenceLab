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
    }
}