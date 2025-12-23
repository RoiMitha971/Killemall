using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DragManager : MonoBehaviour
{
    public static DragManager Instance => _instance;

    private static DragManager _instance;

    [SerializeField]
    private RectTransform _defaultLayer = null;
    [SerializeField]
    private RectTransform _dragLayer = null;

    private List<CardSlot> _slots;

    private Rect _screenBoundingBox;

    private PlayerCard _currentDraggedObject = null;
    public PlayerCard CurrentDraggedObject => _currentDraggedObject;

    private Vector2 _initalPosition;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        _screenBoundingBox = _dragLayer.GetBoundingBoxRect();
    }

    public void RegisterDraggedObject(PlayerCard drag, Vector2 startPos)
    {
        _initalPosition = startPos;
        _currentDraggedObject = drag;
        drag.transform.SetParent(_dragLayer);
        foreach (var slot in _slots)
        {
            slot.RefreshBounds();
        }
    }

    public void UnregisterDraggedObject(PlayerCard drag, Vector2 endPos)
    {
        if (_slots.TryGetFirst(x => x.IsWithinBounds(endPos), out CardSlot slot))
        {
            slot.PlaceCard(drag);
        }
        else
        {
            if(drag.Slot != null)
                drag.Slot.RemoveCard();
            drag.transform.SetParent(_defaultLayer);
        }
        
        _currentDraggedObject = null;
    }

    public bool IsWithinBounds(Vector2 position)
    {
        return _screenBoundingBox.Contains(position);
    }
}



