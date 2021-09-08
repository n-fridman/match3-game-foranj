using UnityEngine;

namespace Match3Game.UI.Buttons
{
    [AddComponentMenu("Match 3 Game/UI/Quit Menu Button")]
    public class QuitMenuButton : MonoBehaviour
    {
        [Header("Components")]
        [Tooltip("Quit menu popup reference.")]
        [SerializeField] private QuitMenuPopup _popup;

        private void Awake()
        {
            if (_popup == null) _popup = FindObjectOfType<QuitMenuPopup>(true);
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