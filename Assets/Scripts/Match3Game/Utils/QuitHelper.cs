using UnityEngine;

namespace Match3Game.Utils
{
    [AddComponentMenu("Match 3 Game/Utils/Quit Helper")]
    public class QuitHelper : MonoBehaviour
    {
        /// <summary>
        /// Close game.
        /// </summary>
        public void QuitGame()
        {
            Debug.Log("{Utils} => [QuitHelper] - (QuitGame) -> Game closed.", gameObject);
            Application.Quit(0); 
        }
    }
}