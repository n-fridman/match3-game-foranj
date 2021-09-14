using UnityEngine;

namespace Match3Game.UI
{
    public abstract class ListItem : MonoBehaviour
    {
        public abstract void OnDraw();

        public abstract void OnDraw(int index);
    }
}