using UnityEngine;

namespace Match3Game.Types
{
    /// <summary>
    /// Cell data
    /// </summary>
    [System.Serializable]
    public struct CellData
    {
        public int scoreCount;
        public CellType type;
        public Color color;
    }
}