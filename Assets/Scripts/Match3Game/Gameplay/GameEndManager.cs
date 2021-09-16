using System;
using Match3Game.Types;
using Match3Game.UI;
using UnityEngine;

namespace Match3Game.Gameplay
{
    public class LoseManager : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private LosePopup _losePopup;

        private void Awake()
        {
            if (_losePopup == null) _losePopup = FindObjectOfType<LosePopup>(true);
        }

        /// <summary>
        /// On game lose event handler.
        /// </summary>
        /// <param name="result">Game result data.</param>
        public void OnGameLose(GameResult result)
        {
            _losePopup.ShowPopup();
        }
    }
}