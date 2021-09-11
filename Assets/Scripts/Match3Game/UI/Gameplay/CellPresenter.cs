using UnityEngine;
using UnityEngine.UI;

namespace Match3Game.UI.Gameplay
{
    [AddComponentMenu("Match 3 Game/Board/Cell Presenter")]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Image))]
    public class CellPresenter : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private Animator _cellAnimator;
        [SerializeField] private Image _cellImage;
        [SerializeField] private Text _cellScoreText;
       
        private static readonly int DestroyTrigger = Animator.StringToHash("Destroy");
        private static readonly int Highlight = Animator.StringToHash("Highlight");

        private void Awake()
        {
            if (_cellAnimator == null) _cellAnimator = GetComponent<Animator>();
            if (_cellImage == null) _cellImage = GetComponent<Image>();
            if (_cellScoreText == null) _cellScoreText = GetComponentInChildren<Text>();
        }

        /// <summary>
        /// Set cell color.
        /// </summary>
        /// <param name="color">Cell color.</param>
        public void SetCellColor(Color color) => _cellImage.color = color;
        
        /// <summary>
        /// Set cell score.
        /// </summary>
        /// <param name="score">Cell score.</param>
        public void SetCellScore(int score) => _cellScoreText.text = score.ToString();
        
        /// <summary>
        /// Play cell destroy animation.
        /// </summary>
        public void PlayDestroyAnimation() => _cellAnimator.SetTrigger(DestroyTrigger);
        
        /// <summary>
        /// Play highlight animation.
        /// </summary>
        public void PlayHighlightAnimation() => _cellAnimator.SetBool(Highlight, true);

        /// <summary>
        /// Disable highlight animation.
        /// </summary>
        public void DisableHighlight() => _cellAnimator.SetBool(Highlight, false);
    }
}