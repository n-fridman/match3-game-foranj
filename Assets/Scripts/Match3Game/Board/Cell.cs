using Match3Game.Types;
using Match3Game.UI.Gameplay;
using UnityEngine;

namespace Match3Game.Board
{
    [AddComponentMenu("Match 3 Game/Board/Cell")]
    [RequireComponent(typeof(CellPresenter))]
    public class Cell : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private CellPresenter _presenter;
        
        [Header("Settings")]
        [SerializeField] private CellData _data;
        public int Score => _data.scoreCount;
        public CellType Type => _data.type;
        
        private void Awake()
        {
            if (_presenter == null) _presenter = GetComponent<CellPresenter>();
        }
        
        /// <summary>
        /// Set cell data.
        /// </summary>
        /// <param name="data"></param>
        public void SetCellData(CellData data)
        {
            _data = data;
            
            _presenter.SetCellColor(data.color);
            _presenter.SetCellScore(data.scoreCount);
        }
        
        /// <summary>
        /// Play cell destroy animation.
        /// </summary>
        public void PlayDestroyAnimation()
        {
            _presenter.PlayDestroyAnimation();
            _presenter.DisableHighlight();
        }

        /// <summary>
        /// Highlight cell.
        /// </summary>
        public void HighlightCell() => _presenter.PlayHighlightAnimation();
        
        /// <summary>
        /// Disable highlight animation.
        /// </summary>
        public void DisableHighlight() => _presenter.DisableHighlight();
    }
}