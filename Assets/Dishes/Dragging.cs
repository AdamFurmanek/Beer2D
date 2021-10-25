using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragging : MonoBehaviour
{
    public float scrollingSpeed;
    Vector2 offset;

    public void OnMouseDown()
    {
        offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.position;
    }

    public void OnMouseDrag()
    {
        if (Input.mouseScrollDelta.y > 0)
            transform.Rotate(0, 0, scrollingSpeed);
        if (Input.mouseScrollDelta.y < 0)
            transform.Rotate(0, 0, -scrollingSpeed);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition - offset;
    }
}
