// TODO: Сделать полноценный ListView ассет.
using System.Collections.Generic;
using UnityEngine;

namespace Match3Game.UI
{
    [AddComponentMenu("Match 3 Game/UI/List View")]
    public class ListView : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private GameObject _itemPrefab;
        [SerializeField] private Transform _containerParentTransform;
        [SerializeField] private RectTransform _containerRectTransform;
        [SerializeField] private List<ListItem> _items;
        [SerializeField] private float _verticalSpacing;

        private int _drawingIndex;
        
        private void Awake()
        {
            _drawingIndex = 0;
        }

        /// <summary>
        /// Draw item to list.
        /// </summary>
        /// <param name="item">Drawing item data.</param>
        public ListItem DrawEmptyListItem()
        {
            _drawingIndex++;
            GameObject itemGm = Instantiate(_itemPrefab, _containerParentTransform);
            ListItem listItem = itemGm.GetComponent<ListItem>();
            _items.Add(listItem);
            
            Rect itemRect = itemGm.GetComponent<RectTransform>().rect;
            Rect containerRect = _containerRectTransform.rect;
            
            float containerHeight = containerRect.height;
            float itemHeight = itemRect.height;
            
            float containerHeightAfterAddingItem = containerHeight + itemHeight + _verticalSpacing;
            _containerRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, containerHeightAfterAddingItem);

            return listItem;
        }
    }
}