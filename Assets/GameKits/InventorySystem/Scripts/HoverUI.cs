using UnityEngine;
using UnityEngine.EventSystems;

namespace GameKits.InventorySystem.Scripts
{
    public class HoverUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] Texture2D cursor;

        public void OnPointerClick(PointerEventData eventData)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Cursor.SetCursor(cursor, new Vector2(13, 4), CursorMode.Auto);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }
    }
}
