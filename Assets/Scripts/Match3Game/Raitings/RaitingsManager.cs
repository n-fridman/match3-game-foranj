using System.Collections.Generic;
using System.IO;
using System.Linq;
using Match3Game.Types;
using UnityEngine;

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
        
        private void Awake()
        {
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

        /// <summary>
        /// Add new game result.
        /// </summary>
        /// <param name="result">Game result structure.</param>
        public GameResult AddGameResult(GameResult result)
        {
            if (_gameResults.Count >= _maxGameResultsCount)
            {
                GameResult lastGameResult = _gameResults.Last();
                _gameResults.Remove(lastGameResult);
                _gameResults.Add(result);
            }
            
            _gameResults.Sort((result1, result2) => {
                if (result1.score > result2.score) return -1;
                if (result1.score < result2.score) return 1;
                return 0;
            });

            return result;
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