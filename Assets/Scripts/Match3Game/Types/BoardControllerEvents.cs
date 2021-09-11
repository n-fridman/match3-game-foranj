using Match3Game.Board;
using UnityEngine.Events;

namespace Match3Game.Types
{
    [System.Serializable]
    public struct BoardControllerEvents
    {
        public UnityEvent onGameStart;
        public UnityEvent<GameResult> onGameEnd;
        public UnityEvent<int> onScoreCountChanged;
        public UnityEvent<int> onPlayerMovesCountChanged;
    }
}