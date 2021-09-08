using System;
using Match3Game.SceneManagement;
using UnityEngine;

namespace Match3Game.UI.Buttons
{
    [AddComponentMenu("Match 3 Game/UI/Load Scene Button")]
    public class LoadSceneButton : MonoBehaviour
    {
        [Header("Components")] 
        [Tooltip("Scene loader reference.")]
        [SerializeField] private SceneLoader _loader;

        [Header("Settings")] 
        [Tooltip("Core game scene name.")]
        [SerializeField] private string _sceneName;

        private void Awake()
        {
            if (_loader == null) _loader = FindObjectOfType<SceneLoader>();
        }

        /// <summary>
        /// On load scene button click event handler.
        /// </summary>
        public void OnClick()
        {
            _loader.LoadScene(_sceneName);
            Debug.Log($"{{UI}} => [LoadSceneButton] - (OnClick) -> {_sceneName} scene loaded.", gameObject);
        }
    }
}