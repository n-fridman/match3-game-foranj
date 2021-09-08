using UnityEngine;

namespace Match3Game.UI
{
    [AddComponentMenu("Match 3 Game/UI/Popup")]
    public class Popup : MonoBehaviour
    {
        [Header("Popup game object")] 
        [SerializeField] private GameObject _popupGameObject;
        
        /// <summary>
        /// Show popup.
        /// </summary>
        public void ShowPopup() => _popupGameObject.SetActive(true);
        
        /// <summary>
        /// Hide popup.
        /// </summary>
        public void HidePopup() => _popupGameObject.SetActive(false);
    }
}