using System;

namespace Match3Game.Types
{
    [Serializable]
    public struct GameResult
    {
        public DateTime gameDateTime;
        public int scoreCount;
    }
}