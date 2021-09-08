using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Match3Game.SceneManagement
{
    [AddComponentMenu("Match 3 Game/Utils/Scene Loader")]
    public class SceneLoader : MonoBehaviour
    {
        /// <summary>
        /// Game scene content type.
        /// </summary>
        [System.Serializable]
        private enum GameSceneType
        {
            Core = 0,
            Meta = 1
        }
        
        /// <summary>
        /// Game scene metadata.
        /// </summary>
        [System.Serializable]
        private struct GameScene
        {
            public string name;
            public GameSceneType type;
            public bool isLoaded;
        }

        [Header("Settings")] 
        [SerializeField] private List<GameScene> _scenes;
        [SerializeField] private bool _coreSceneLoaded;
        [SerializeField] private GameScene _loadedCoreScene;
        
        /// <summary>
        /// Scene loaded event handler.
        /// </summary>
        /// <param name="scene">Loaded scene.</param>
        /// <param name="mode">Load scene mode.</param>
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (_scenes.Exists(s => s.name == scene.name) == false) return;
            
            GameScene gameSceneFromList = _scenes.Find(s => s.name == scene.name);
            int index = _scenes.IndexOf(gameSceneFromList);
            if (_scenes.Remove(gameSceneFromList))
            {
                GameScene newGameScene = new GameScene
                {
                    name = scene.name,
                    isLoaded = true,
                    type = gameSceneFromList.type
                };

                _scenes.Insert(index, newGameScene);
            }
        }
        
        /// <summary>
        /// Scene unloaded event handler.
        /// </summary>
        /// <param name="scene">Unloaded scene.</param>
        private void OnSceneUnloaded(Scene scene)
        {
            if (_scenes.Exists(s => s.name == scene.name) == false) return;
            GameScene gameSceneFromList = _scenes.Find(s => s.name == scene.name);
            int index = _scenes.IndexOf(gameSceneFromList);
            if (_scenes.Remove(gameSceneFromList))
            {
                GameScene newGameScene = new GameScene
                {
                    name = scene.name,
                    isLoaded = true,
                    type = gameSceneFromList.type
                };

                _scenes.Insert(index, newGameScene);
            }
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private void Awake()
        {
            List<GameScene> scenes = _scenes;

            foreach (GameScene scene in scenes)
            {
                if (scene.isLoaded) continue;

                switch (scene.type)
                {
                    case GameSceneType.Core:
                        if (_coreSceneLoaded) continue;
                        
                        SceneManager.LoadScene(scene.name, LoadSceneMode.Additive);
                        _loadedCoreScene = scene;
                        _coreSceneLoaded = true;
                        break;
                    
                    case GameSceneType.Meta:
                        SceneManager.LoadScene(scene.name, LoadSceneMode.Additive);
                        break;
                }
            }
        }
        
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        /// <summary>
        /// Load game scene.
        /// </summary>
        /// <param name="sceneName">Game scene name.</param>
        public void LoadScene(string sceneName)
        {
            if (_scenes.Exists(s => s.name == sceneName))
            {
                GameScene gameScene = _scenes.Find(s => s.name == sceneName);

                SceneManager.LoadScene(gameScene.name, LoadSceneMode.Additive);
                SceneManager.UnloadSceneAsync(_loadedCoreScene.name);
                _loadedCoreScene = gameScene;
            }
        }
    }
}
