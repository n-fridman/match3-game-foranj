using System;

namespace Match3Game.Board
{
    [Serializable]
    public struct GameResult
    {
        public DateTime gameDateTime;
        public int scoreCount;
    }
}