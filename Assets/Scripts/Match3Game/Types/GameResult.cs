using System;

namespace Match3Game.Types
{
    [Serializable]
    public struct GameResult
    {
        public string date;
        public DateTime gameDateTime;
        public int score;
    }
}