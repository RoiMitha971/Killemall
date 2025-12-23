using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PlayerCard<T> : PlayerCard
{

    protected T _context;

    public abstract void Activate(T context);
}

public abstract class PlayerCard : Card, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public CardSlot Slot => _currentSlot;
    private CardSlot _currentSlot;
    private Vector2 _worldCenterPoint => transform.TransformPoint((transform as RectTransform).rect.center);

    public virtual void SetSlot(CardSlot slot)
    {
        _currentSlot = slot;
        if (slot != null)
        {
            transform.SetParent(_currentSlot.transform);
            transform.localPosition = Vector3.zero;
        }
    }

    public virtual void Draw()
    {

    }
    public virtual void Discard()
    {

    }
    public abstract void Use();

    public virtual void OnBeginDrag(PointerEventData data)
    {
        DragManager.Instance.RegisterDraggedObject(this, _worldCenterPoint);
    }
    public virtual void OnDrag(PointerEventData data)
    {
        if (DragManager.Instance.IsWithinBounds(_worldCenterPoint + data.delta))
        {
            transform.Translate(data.delta);
        }
    }
    public virtual void OnEndDrag(PointerEventData data)
    {
        DragManager.Instance.UnregisterDraggedObject(this, _worldCenterPoint);
    }

}