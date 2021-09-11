using System;
using UnityEngine;
using UnityEngine.UI;

namespace Match3Game.UI.Gameplay
{
    [AddComponentMenu("Match 3 Game/UI/Presenters/Gameplay/Score Presenter")]
    [RequireComponent(typeof(Text))]
    public class ScorePresenter : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private Text _scoreText;

        private void Awake()
        {
            if (_scoreText == null) _scoreText = GetComponent<Text>();
        }

        /// <summary>
        /// Score count changed event handler.
        /// </summary>
        /// <param name="scoreCount">Score count.</param>
        public void OnScoreCountChanged(int scoreCount)
        {
            _scoreText.text = scoreCount.ToString("0000");
        }
    }
}