using System.Collections.Generic;
using Match3Game.Types;
using Match3Game.UI;
using Match3Game.UI.Raitings;
using UnityEngine;

namespace Match3Game.Raitings
{
    public class RaitingsDrawer : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private ListView _listView;

        [Header("Settings")] 
        [SerializeField] private Color _top1RowColor;
        [SerializeField] private Color _top2RowColor;
        [SerializeField] private Color _top3RowColor;
        
        /// <summary>
        /// Draw raitings list to screen.
        /// </summary>
        /// <param name="raitingsList">Game result list.</param>
        public void DrawRaitings(List<GameResult> raitingsList)
        {
            for (int i = 0; i < raitingsList.Count; i++)
            {
                GameResult gameResult = raitingsList[i];
                ListItem item = _listView.DrawEmptyListItem();
                RaitingsRowPresenter presenter = item.GetComponent<RaitingsRowPresenter>();
                presenter.SetRowData(gameResult);

                switch (i)
                {
                    case 0:
                        presenter.SetRowColor(_top1RowColor);
                        break;
                    
                    case 1:
                        presenter.SetRowColor(_top2RowColor);
                        break;
                    
                    case 2:
                        presenter.SetRowColor(_top3RowColor);
                        break;
                }

                if (gameResult.highlight) presenter.HighlightRow();
            }
        }
    }
}