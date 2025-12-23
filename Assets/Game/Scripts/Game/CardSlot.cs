using UnityEngine;

public class CardSlot : MonoBehaviour
{
    private PlayerCard _currentCard;

    private Rect _bounds;

    public void Start()
    {
        RefreshBounds();
    }

    public void PlaceCard(PlayerCard card)
    {
        CardSlot otherSlot = card.Slot;
        if (otherSlot != null)
        {
            otherSlot.RemoveCard();
            //Swap Cards
            if (_currentCard != null)
            {
                otherSlot.PlaceCard(_currentCard);
            }
        }
        else
        {
            RemoveCard();
        }

        _currentCard = card;
        _currentCard.SetSlot(this);
    }

    public void RemoveCard()
    {
        if(_currentCard != null)
        {
            _currentCard.SetSlot(null);
        }
        _currentCard = null;
    }

    public void RefreshBounds()
    {
        _bounds = (transform as RectTransform).GetBoundingBoxRect();
    }

    public bool IsWithinBounds(Vector2 position)
    {
        return _bounds.Contains(position);
    }
}
