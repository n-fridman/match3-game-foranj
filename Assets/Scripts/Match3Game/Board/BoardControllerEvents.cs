using UnityEngine.Events;

namespace Match3Game.Board
{
    [System.Serializable]
    public struct BoardControllerEvents
    {
        public UnityEvent onGameStart;
        public UnityEvent onGameEnd;
        public UnityEvent<int> onScoreCountChanged;
        public UnityEvent<int> onPlayerMovesCountChanged;
    }
}