using Match3Game.Types;
using UnityEngine;
using UnityEngine.UI;

namespace Match3Game.UI.Raitings
{
    public class RaitingsRowPresenter : ListItem
    {
        [Header("Components")] 
        [SerializeField] private Text _scoreText;
        [SerializeField] private Text _dateText;
        [SerializeField] private Animator _rowAnimator;
        [SerializeField] private Image _rowIconBorderImage;
        
        private static readonly int Highlight = Animator.StringToHash("Highlight");
        
        /// <summary>
        /// Draw row data.
        /// </summary>
        /// <param name="gameResult">Game result</param>
        public void SetRowData(GameResult gameResult)
        {
            _scoreText.text = gameResult.score.ToString();
            _dateText.text = gameResult.date;
        }
        
        /// <summary>
        /// Set row icon border color.
        /// </summary>
        /// <param name="color">Color</param>
        public void SetRowColor(Color color)
        {
            _rowAnimator.enabled = false;
            _rowIconBorderImage.color = color;
        }

        /// <summary>
        /// Highlight raitings row.
        /// </summary>
        public void HighlightRow() => _rowAnimator.SetTrigger(Highlight);
    }
}