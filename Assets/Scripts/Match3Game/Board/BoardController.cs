using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Match3Game.InputSystem;
using Match3Game.Types;
using UnityEngine;

namespace Match3Game.Board
{
    [AddComponentMenu("Match 3 Game/Board/Board Controller")]
    [RequireComponent(typeof(BoardGenerator))]
    public class BoardController : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private BoardGenerator _generator;
        [SerializeField] private CoreGameplayInput _input;

        [Header("Settings")] 
        [Tooltip("Game board size.")]
        [SerializeField] private Vector2Int _boardSize;
        
        [Tooltip("Current game session score count.")]
        [SerializeField] private int _scoreCount;
        
        [Tooltip("Remaining moves count.")]
        [SerializeField] private int _movesCount;
        
        [Tooltip("Delay for playing cell destroy animation.")]
        [SerializeField] [Range(0, 1)] private float _updateBoardDelay;
        [SerializeField] private List<Cell> _detachedCells;
        
        [Header("Events")] 
        public BoardControllerEvents events;

        private CellBg[,] _board;

        /// <summary>
        /// Check exist empty cells on board.
        /// </summary>
        /// <returns>True if empty cells exist.</returns>
        private bool IsEmptyCellsExist()
        {
            for (int x = 0; x < _boardSize.x; x++)
            {
                for (int y = 0; y < _boardSize.y; y++)
                {
                    CellBg cellBg = _board[x, y];
                    if (cellBg.IsLinked == false) return true;
                }
            }

            return false;
        }
        
        /// <summary>
        /// Return matched cells for clicked cell bg.
        /// </summary>
        /// <param name="clickedCellBg">Clicked cell bg.</param>
        /// <returns>Matched cell bgs</returns>
        private List<CellBg> GetMatchedCellBgs(CellBg clickedCellBg)
        {
            Vector2Int cellPos = clickedCellBg.PositionInGrid;
            Cell cell = clickedCellBg.GetLinkedCell();

            List<CellBg> matchedCellBgs = new List<CellBg>();
            List<CellBg> xAxisMatchedCellBgs = new List<CellBg>();
            List<CellBg> yAxisMatchedCellBgs = new List<CellBg>();
            
            matchedCellBgs.Add(clickedCellBg);
            for (int x = cellPos.x +  1; x < _boardSize.x; x++)
            {
                CellBg candidateCellBg = _board[x, cellPos.y];
                Cell candidateCell = candidateCellBg.GetLinkedCell();
                if (cell.Type == candidateCell.Type) xAxisMatchedCellBgs.Add(candidateCellBg);
                else break;
            }
            for (int x = cellPos.x - 1; x >= 0; x--)
            {
                CellBg candidateCellBg = _board[x, cellPos.y];
                Cell candidateCell = candidateCellBg.GetLinkedCell();
                if (cell.Type == candidateCell.Type) xAxisMatchedCellBgs.Add(candidateCellBg);
                else break;
            }
            for (int y = cellPos.y + 1; y < _boardSize.x; y++)
            {
                CellBg candidateCellBg = _board[cellPos.x, y];
                Cell candidateCell = candidateCellBg.GetLinkedCell();
                if (cell.Type == candidateCell.Type) yAxisMatchedCellBgs.Add(candidateCellBg);
                else break;
            }
            for (int y = cellPos.y - 1; y >= 0; y--)
            {
                CellBg candidateCellBg = _board[cellPos.x, y];
                Cell candidateCell = candidateCellBg.GetLinkedCell();
                if (cell.Type == candidateCell.Type) yAxisMatchedCellBgs.Add(candidateCellBg);
                else break;
            }

            if (xAxisMatchedCellBgs.Count >= 2) matchedCellBgs.AddRange(xAxisMatchedCellBgs);
            if (yAxisMatchedCellBgs.Count >= 2) matchedCellBgs.AddRange(yAxisMatchedCellBgs);
            
            return matchedCellBgs;
        }

        /// <summary>
        /// Return bottom empty cell background.
        /// </summary>
        /// <param name="startX">X-axis start position.</param>
        /// <param name="startY">Y-axis start position.</param>
        /// <returns>Empty cell background.</returns>
        private CellBg GetBottomEmptyCellBg(int startX, int startY)
        {
            CellBg candidateCellBg = null;
            
            for (int y = startY; y < _boardSize.y; y++)
            {
                CellBg tempCellBg = _board[startX, y];
                if (tempCellBg.IsLinked == false) candidateCellBg = tempCellBg;
                else break;
            }

            return candidateCellBg;
        }
        
        /// <summary>
        /// Move all cells on game board.
        /// </summary>
        private void MoveCells()
        {
            for (int x = _boardSize.x - 1; x >= 0; x--)
            {
                for (int y = _boardSize.y - 1; y >= 0; y--)
                {
                    CellBg cellBg = _board[x, y];
                    if (cellBg.IsLinked == false) continue;

                    CellBg cellBgToMove = GetBottomEmptyCellBg(cellBg.PositionInGrid.x, cellBg.PositionInGrid.y + 1);
                    if (cellBgToMove == null) continue; 

                    Cell cell = cellBg.GetLinkedCell();
                    cellBg.DetachLinkedCell();
                    cellBgToMove.SetLinkedCell(cell);
                    cell.transform.SetParent(cellBgToMove.transform);

                    LeanTween.moveLocal(cell.gameObject, Vector3.zero, 0.25f);
                }
            }
        }
        
        /// <summary>
        /// Spawn and move cells.
        /// </summary>
        private void SpawnAndMoveCells()
        {
            for (int x = 0; x < _boardSize.x; x++)
            {
                CellBg cellBg = _board[x, 0];
                if (cellBg.IsLinked == false)
                {
                    CellData cellData = _generator.GenerateCellData();
                    
                    Vector3 newCellPosition = cellBg.transform.localPosition;
                    newCellPosition.y += _generator.cellBgHeight;

                    Cell cell = _detachedCells.First();
                    _detachedCells.Remove(cell);
                    cell.transform.localPosition = newCellPosition;
                    cell.gameObject.SetActive(true);

                    CellBg targetCellBg = GetBottomEmptyCellBg(cellBg.PositionInGrid.x, cellBg.PositionInGrid.y);
                    cell.transform.SetParent(targetCellBg.transform);
                    targetCellBg.SetLinkedCell(cell);
                    cell.SetCellData(cellData);
                    
                    LeanTween.move(cell.gameObject, targetCellBg.transform.position, 0.25f);
                }
            }
        }
        
        /// <summary>
        /// Update cells on board.
        /// </summary>
        private IEnumerator UpdateGameBoard()
        {
            yield return new WaitForSeconds(_updateBoardDelay);
            MoveCells();
            while (IsEmptyCellsExist())
            {
                SpawnAndMoveCells();
            }
            _input.UnsetPause();
        }
        
        private void Awake()
        {
            if (_generator == null) _generator = GetComponent<BoardGenerator>();
            if (_input == null) _input = FindObjectOfType<CoreGameplayInput>();

            _board = _generator.GenerateBoard(_boardSize.x, _boardSize.y);
            
            events.onScoreCountChanged?.Invoke(_scoreCount);
            events.onPlayerMovesCountChanged?.Invoke(_movesCount);
            events.onGameStart?.Invoke();
        }

        public void DestroyCell(CellBg cellBg)
        {
            _input.SetPause();
            
            _movesCount--;

            Debug.Log($"{{Board}} => [BoardController] - (DestroyCell) -> Cell destroyed.", cellBg.gameObject);

            List<CellBg> matchedCellBgs = GetMatchedCellBgs(cellBg);
            List<Cell> matchedCells = new List<Cell>();
            
            foreach (CellBg matchedCellBg in matchedCellBgs)
            {
                Cell cell = matchedCellBg.GetLinkedCell();
                matchedCellBg.DetachLinkedCell();

                _scoreCount += cell.Score;
                
                cell.transform.SetParent(transform);
                cell.PlayDestroyAnimation();
                _detachedCells.Add(cell);
                
                matchedCells.Add(cell);
            }
            
            if (matchedCells.Count >= 3)
            {
                _movesCount += matchedCells.Count - 1;
                Debug.Log($"{{Board}} => [BoardController] - (DestroyCell) -> Added {matchedCells.Count - 1} moves count.");
            }
            events.onPlayerMovesCountChanged?.Invoke(_movesCount);
            events.onScoreCountChanged?.Invoke(_scoreCount);
            if (_movesCount == 0)
            {
                events.onGameEnd?.Invoke();
            }
            
            StartCoroutine(UpdateGameBoard());
        }
    }
}