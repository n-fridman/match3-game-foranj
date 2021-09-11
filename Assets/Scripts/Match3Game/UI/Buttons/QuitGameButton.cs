using UnityEngine;

namespace Match3Game.UI.Buttons
{
    [AddComponentMenu("Match 3 Game/UI/Quit Game Button")]
    public class QuitGameButton : MonoBehaviour
    {
        [Header("Components")]
        [Tooltip("Exit game popup reference.")]
        [SerializeField] private ExitGamePopup _popup;

        private void Awake()
        {
            if (_popup == null) _popup = FindObjectOfType<ExitGamePopup>(true);
        }
        
        /// <summary>
        /// On quit menu button click event handler.
        /// </summary>
        public void OnClick()
        {
            _popup.ShowPopup();
        }
    }
}