using System;
using System.Collections.Generic;

namespace Utils
{
    [Serializable]
    public class GameState
    {
        public bool HasPickedCoinBefore;
        public bool HasPickedCollectibleBefore;
        public List<int> Checkpoints = new();
        public List<int> PickedUpCollectibles = new();
        public string Username;

        public void Reset()
        {
            HasPickedCoinBefore = false;
            HasPickedCollectibleBefore = false;
            Checkpoints.Clear();
            PickedUpCollectibles.Clear();
            Username = null;
        }
    }
}