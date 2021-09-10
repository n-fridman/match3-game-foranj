using Match3Game.Board;
using UnityEngine;

namespace Match3Game.InputSystem
{
    public class CoreGameplayInput : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private BoardController _boardController;

        [Header("Settings")] 
        [Tooltip("Pause input.")]
        [SerializeField] private bool _pause;
        
        private CellBg GetCellBgForPosition(Vector2 position)
        {
            Ray ray = _mainCamera.ScreenPointToRay(position);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 1);
                if (hit.collider)
                {
                    return hit.collider.gameObject.GetComponent<CellBg>();
                }
            }

            return null;
        }
        
        private void Awake()
        {
            if (_boardController == null) _boardController = FindObjectOfType<BoardController>();
        }

        private void Update()
        {
            if (_pause) return;

            if (Input.GetMouseButtonDown(0))
            {
                CellBg cellBg = GetCellBgForPosition(Input.mousePosition);
                if (cellBg == null) return;
                _boardController.DestroyCell(cellBg);
            }
        }
        
        /// <summary>
        /// Set pause.
        /// </summary>
        public void SetPause() => _pause = true;
        
        /// <summary>
        /// Continue.
        /// </summary>
        public void UnsetPause() => _pause = false;
    }
}