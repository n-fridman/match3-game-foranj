using UnityEngine;

namespace Match3Game.Board
{
    [AddComponentMenu("Match 3 Game/Board/Cell Bg")]
    public class CellBg : MonoBehaviour
    {
        [Header("Components")] 
        [Tooltip("Linked cell.")]
        [SerializeField] private Cell _linkedCell;

        [Tooltip("True if linked cell exist.")]
        [SerializeField] private bool _isLinked;
        public bool IsLinked => _isLinked;
        
        /// <summary>
        /// Set new linked cell.
        /// </summary>
        /// <param name="cell">Cell</param>
        public void SetLinkedCell(Cell cell)
        {
            _linkedCell = cell;
            cell.transform.SetParent(transform);
            _isLinked = true;
        }
        
        /// <summary>
        /// Return linked cell.
        /// </summary>
        /// <returns>Linked cell.</returns>
        public Cell GetLinkedCell() => _linkedCell;

        /// <summary>
        /// Detach linked cell.
        /// </summary>
        public void DetachLinkedCell() => _isLinked = false;
    }
}