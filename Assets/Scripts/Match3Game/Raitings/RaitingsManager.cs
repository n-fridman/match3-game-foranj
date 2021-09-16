using System.Collections.Generic;
using System.IO;
using System.Linq;
using Match3Game.SceneManagement;
using Match3Game.Types;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Match3Game.Raitings
{
    [AddComponentMenu("Match 3 Game/Raitings/Raitings Manager")]
    public class RaitingsManager : MonoBehaviour
    {
        [System.Serializable]
        private struct GameResultsListWrapper
        {
            public List<GameResult> results;
        }

        [Header("Components")] 
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private RaitingsDrawer _raitingsDrawer;

        [Header("Game results")] 
        [SerializeField] private List<GameResult> _gameResults;

        [Header("Settings")] 
        [Tooltip("Maximum game results count stored.")]
        [SerializeField] private int _maxGameResultsCount;
        
        [Tooltip("Key for saving data to player prefs.")]
        [SerializeField] private string _playerPrefsSaveKey;

        [Tooltip("If game started in first time raitings will be loaded from this resource file.")]
        [SerializeField] private string _defaultRaitingsResourceName;
        
        /// <summary>
        /// Save raitings table to player prefs.
        /// </summary>
        private void SaveGameResults()
        {
            GameResultsListWrapper gameResultsWrapper = new GameResultsListWrapper{
                results = _gameResults,
            };
            string json = JsonUtility.ToJson(gameResultsWrapper);
            PlayerPrefs.SetString(_playerPrefsSaveKey, json);
            Debug.Log("{Raitings} => [RaitingsManager] - (SaveGameResults) -> Raitings table data saved to player prefs.");
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            switch (scene.name)
            {
                case "Raitings":
                    _raitingsDrawer = FindObjectOfType<RaitingsDrawer>();
                    _raitingsDrawer.DrawRaitings(_gameResults);
                    GameResult highlightedGameResult = _gameResults.Find(g => g.highlight);
                    if (highlightedGameResult.highlight)
                    {
                        int index = _gameResults.IndexOf(highlightedGameResult);
                        if (_gameResults.Remove(highlightedGameResult))
                        {
                            highlightedGameResult.highlight = false;
                            Debug.Log("DDD");
                            _gameResults.Insert(index, highlightedGameResult);
                        }
                    }
                    break;
            }
        }
        
        /// <summary>
        /// Sort game results in collection.
        /// </summary>
        private void Sort()
        {
            _gameResults.Sort((result1, result2) => {
                if (result1.score > result2.score) return -1;
                if (result1.score < result2.score) return 1;
                return 0;
            });
        }
        
        private void Awake()
        {
            if (_sceneLoader == null) _sceneLoader = FindObjectOfType<SceneLoader>(true);

            if (PlayerPrefs.HasKey(_playerPrefsSaveKey))
            {
                string raitingsJson = PlayerPrefs.GetString(_playerPrefsSaveKey);
                GameResultsListWrapper gameResultsWrapper = JsonUtility.FromJson<GameResultsListWrapper>(raitingsJson);
                _gameResults = gameResultsWrapper.results;
                
                Debug.Log("{Raitings} => [RaitingsManager] - (Awake) -> Raitings table loaded from saved data.");
            }
            else
            {
                TextAsset raitingsCsvTextAsset = Resources.Load<TextAsset>(_defaultRaitingsResourceName);
                if (raitingsCsvTextAsset == null) throw new FileNotFoundException("Resource file not found. Please check resource name and try again.");

                string csvText = raitingsCsvTextAsset.text;
                _gameResults = CSVSerializer.Deserialize<GameResult>(csvText).ToList();
                
                Debug.Log("{Raitings} => [RaitingsManager] - (Awake) -> Raitings table loaded from resource file.");
            }
            
            Sort();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnApplicationPause(bool pauseStatus)
        {
#if UNITY_ANDROID
            SaveGameResults();
#endif
        }

        private void OnApplicationQuit()
        {
            SaveGameResults();
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        /// <summary>
        /// Add new game result.
        /// </summary>
        /// <param name="result">Game result structure.</param>
        /// <returns>True if result added to raitings table.</returns>
        public bool AddGameResult(GameResult result)
        {
            GameResult lastGameResult = _gameResults.Last();
            if (lastGameResult.score < result.score)
            {
                _gameResults.Remove(lastGameResult);
                result.highlight = true;
                _gameResults.Add(result);
                    
                Sort();

                return true;
            }

            return false;
        }
        
        /// <summary>
        /// Return minimal score count in raitings table.
        /// </summary>
        /// <returns>Minimal score count in raitings table.</returns>
        public int GetMinResultScore()
        {
            GameResult lastGameResult = _gameResults.Last();
            return lastGameResult.score;
        }
    }
}