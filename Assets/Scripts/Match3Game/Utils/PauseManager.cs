using UnityEngine;

namespace Match3Game.Utils
{
    public class PauseManager : MonoBehaviour
    {
        /// <summary>
        /// Set pause.
        /// </summary>
        public void SetPause() => Time.timeScale = 0;
        
        /// <summary>
        /// Unset pause.
        /// </summary>
        public void UnsetPause() => Time.timeScale = 1;
    }
}