using UnityEngine;

public static class RectTransformExtensions
{
    public static Rect GetBoundingBoxRect(this RectTransform rectTransform)
    {
        var corners = new Vector3[4]; 
        rectTransform.GetWorldCorners(corners);
        var position = corners[0];

        Vector2 size = new Vector2(
            rectTransform.lossyScale.x * rectTransform.rect.size.x,
            rectTransform.lossyScale.y * rectTransform.rect.size.y
            );

        return new Rect(position, size);
    }
}
