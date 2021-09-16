using Match3Game.Raitings;
using Match3Game.SceneManagement;
using Match3Game.Types;
using Match3Game.UI;
using UnityEngine;

namespace Match3Game.Gameplay
{
    public class GameEndManager : MonoBehaviour
    {
        [Header("Components")] 
        [SerializeField] private LosePopup _losePopup;
        [SerializeField] private RaitingsManager _raitingsManager;
        [SerializeField] private SceneLoader _loader;
        
        private void Awake()
        {
            if (_losePopup == null) _losePopup = FindObjectOfType<LosePopup>(true);
            if (_raitingsManager == null) _raitingsManager = FindObjectOfType<RaitingsManager>();
            if (_loader == null) _loader = FindObjectOfType<SceneLoader>();
        }

        /// <summary>
        /// On game lose event handler.
        /// </summary>
        /// <param name="result">Game result data.</param>
        public void OnGameLose(GameResult result)
        {
            if (_raitingsManager.AddGameResult(result))
            {
                _loader.LoadScene("Raitings");
            }
            else
            {
                _losePopup.ShowPopup();
            }
        }
    }
}