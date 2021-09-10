using System;
using System.Collections.Generic;
using Match3Game.Types;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Match3Game.Board
{
    [AddComponentMenu("Match 3 Game/Board/Board Generator")]
    public class BoardGenerator : MonoBehaviour
    {
        [Serializable]
        private struct CellMeta
        {
            [SerializeField] private string _name;
            public int _minCellScoreCount;
            public int _maxCellScoreCount;
            public Color _cellColor;
            public CellType _type;
        }
        
        [Header("Components")] 
        [SerializeField] private Transform _boardContainerTransform;
        [SerializeField] private GameObject _cellBgPrefab;
        [SerializeField] private GameObject _cellPrefab;

        [Header("Settings")] 
        [SerializeField] private List<CellMeta> _cells;
        public float cellBgWidth;
        public float cellBgHeight;
        
        /// <summary>
        /// Generate new cell data.
        /// </summary>
        /// <returns>Cell data.</returns>
        public CellData GenerateCellData()
        {
            int index = Random.Range(0, _cells.Count);
            CellMeta cellMeta = _cells[index];
            int scoreCount = Random.Range(cellMeta._minCellScoreCount, cellMeta._maxCellScoreCount);
            CellData cellData = new CellData {
                color = cellMeta._cellColor,
                scoreCount = scoreCount,
                type = cellMeta._type
            };
            return cellData;
        }

        /// <summary>
        /// Generate game board.
        /// </summary>
        /// <param name="sizeX">Board size of X axis.</param>
        /// <param name="sizeY">Board size of Y axis.</param>
        /// <returns>Generated board</returns>
        public CellBg[,] GenerateBoard(int sizeX, int sizeY)
        {
            CellBg[,] boardData = new CellBg[sizeX, sizeY];

            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    GameObject cellBgGameObject = Instantiate(_cellBgPrefab, _boardContainerTransform, false);
                    cellBgGameObject.transform.localPosition += new Vector3 {
                        x = x * cellBgWidth,
                        y = -(y * cellBgHeight),
                        z = 0,
                    };
                    CellBg cellBg = cellBgGameObject.GetComponent<CellBg>();
                    
                    GameObject cellGameObject = Instantiate(_cellPrefab, cellBgGameObject.transform, false);
                    Cell cell = cellGameObject.GetComponent<Cell>();
                    CellData cellData = GenerateCellData();
                    
                    cell.SetCellData(cellData);
                    cellBg.SetLinkedCell(cell);
                    cellBg.SetPositionInGrid(x, y);
                    boardData[x, y] = cellBg;
                }
            }   
            
            Debug.Log($"{{Board}} => [BoardGenerator] - (GenerateBoard) -> Game board {sizeX}x{sizeY} generated.");
            return boardData;
        }
    }
}