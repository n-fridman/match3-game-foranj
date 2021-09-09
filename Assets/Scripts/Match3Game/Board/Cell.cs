using Match3Game.Types;
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

        private void Awake()
        {
            if (_presenter == null) _presenter = GetComponent<CellPresenter>();
        }

        public void SetCellData(CellData data)
        {
            _data = data;
            
            _presenter.SetCellColor(data.color);
            _presenter.SetCellScore(data.scoreCount);
        }
    }
}