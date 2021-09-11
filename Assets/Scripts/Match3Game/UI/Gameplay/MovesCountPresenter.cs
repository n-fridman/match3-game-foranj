using UnityEngine;
using UnityEngine.UI;

namespace Match3Game.UI.Gameplay
{
    [AddComponentMenu("Match 3 Game/UI/Presenters/Gameplay/Moves Count Presenter")]
    [RequireComponent(typeof(Text))]
    public class MovesCountPresenter : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private Text _movesCountText;

        private void Awake()
        {
            if (_movesCountText == null) _movesCountText = GetComponent<Text>();
        }
        
        /// <summary>
        /// Player moves count changed event handler.
        /// </summary>
        /// <param name="movesCount">Moves count.</param>
        public void OnPlayerMovesCountChanged(int movesCount)
        {
            _movesCountText.text = movesCount.ToString("00");
        }
    }
}